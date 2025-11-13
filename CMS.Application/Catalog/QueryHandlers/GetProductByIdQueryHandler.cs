using CMS.Application.Catalog.Dtos;
using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Media;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Media.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetProductByIdQueryHandler
        : IAppRequestHandler<GetProductByIdQuery, ResultModel<ProductDto?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;
        private readonly IMediaStorageStrategy _mediaStorageStrategy;

        public GetProductByIdQueryHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext,
            IMediaStorageStrategy mediaStorageStrategy)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
            _mediaStorageStrategy = mediaStorageStrategy;
        }

        public async Task<ResultModel<ProductDto?>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var tenantId = _tenantContext.TenantId;
            var productRepo = _unitOfWork.GetRepository<Product>();
            var mediaAttachRepo = _unitOfWork.GetRepository<MediaAttachment>();

            // 🔹 ۱) لود محصول
            var product = await productRepo.GetAsync(
                predicate: p => p.Id == request.Id && p.WebSiteId == tenantId,
                include: p => p
                    .Include(p => p.Translations)
                    .Include(p => p.Brand)
                        .ThenInclude(b => b.Translations)
                    .Include(p => p.Product_ProductCategories)
                        
                    .Include(p => p.ProductProductAttributes)
                        .ThenInclude(ppa => ppa.ProductAttribute)
                            .ThenInclude(attr => attr.Translations)
                    .Include(p => p.ProductProductAttributes)
                        .ThenInclude(ppa => ppa.ProductAttribute)
                            .ThenInclude(attr => attr.Values)
                                .ThenInclude(v => v.Translations)
                    .Include(p => p.ProductProductAttributes)
                        .ThenInclude(ppa => ppa.ValueMappings)
                            .ThenInclude(vm => vm.ProductAttributeValue)
            );

            if (product == null)
                return ResultModel<ProductDto?>.Fail("Product not found.");

            var attachments = await mediaAttachRepo.GetAllAsync(
                                                        predicate: x => x.EntityId == product.Id && x.EntityType == "Product" && x.TenantId == tenantId,
                                                        func: x => x.Include(y => y.MediaFile));

            // مرتب‌سازی: عکس اصلی اول
            var imageUrls = attachments
                .OrderByDescending(a => a.Purpose == "Main")
                .Select(a => a.MediaFile != null
                    ? _mediaStorageStrategy.GetUrl(a.MediaFile.Key)
                    : string.Empty)
                .Where(url => !string.IsNullOrEmpty(url))
                .ToList();

            // 🔹 ۳) ترجمه‌ها و سایر اطلاعات
            var prodTranslation = product.Translations.FirstOrDefault();
            var brandTranslation = product.Brand?.Translations.FirstOrDefault();
            var categoryTranslation = product.Product_ProductCategories.FirstOrDefault().ProductCategory?.Translations.FirstOrDefault();

            // 🔹 ۴) ویژگی‌ها
            var attributes = product.ProductProductAttributes
                .Where(ppa => ppa.ValueMappings.Any())
                .Select(ppa =>
                {
                    var attr = ppa.ProductAttribute;
                    var mappedValues = ppa.ValueMappings
                        .Where(vm => vm.ProductAttributeValue != null)
                        .Select(vm => vm.ProductAttributeValue!)
                        .ToList();

                    return new ProductAttributeDto
                    {
                        Id = attr.Id,
                        AllowMultipleValues = attr.AllowMultipleValues,
                        Translations = attr.Translations
                            .Select(t => new ProductAttributeTranslationDto
                            {
                                WebSiteLanguageId = t.WebSiteLanguageId,
                                Name = t.Name
                            })
                            .ToList(),
                        Values = mappedValues
                            .Select(v => new ProductAttributeValueDto
                            {
                                Id = v.Id,
                                AttributeId = v.ProductAttributeId,
                                Translations = v.Translations
                                    .Select(tv => new ProductAttributeValueTranslationDto
                                    {
                                        LanguageId = tv.WebSiteLanguageId,
                                        Value = tv.Value
                                    })
                                    .ToList(),
                                IsSelected = true
                            })
                            .ToList()
                    };
                })
                .ToList();

            // 🔹 ۵) ساخت DTO نهایی
            var dto = new ProductDto
            {
                Id = product.Id,
                SKU = product.SKU,
                Name = prodTranslation?.Name ?? string.Empty,
                Description = prodTranslation?.Description ?? string.Empty,
                Slug = prodTranslation?.Slug ?? string.Empty,
                WebSiteLanguageId = prodTranslation?.WebSiteLanguageId ?? 0,
                BrandId = product.BrandId,
                BrandTitle = brandTranslation?.Title ?? "[بدون برند]",
                Barcode = product.Barcode,
                CategoryId = product.Product_ProductCategories.FirstOrDefault().ProductCategory.Id,
                CategoryTitle = categoryTranslation?.Title ?? "[بدون دسته‌بندی]",
                Price = product.Price,
                ImageUrls = imageUrls,
                Attributes = attributes
            };

            return ResultModel<ProductDto?>.Success(dto);
        }
    }
}
