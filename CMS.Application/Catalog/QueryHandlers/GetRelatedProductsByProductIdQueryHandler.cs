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
    public class GetRelatedProductsByProductIdQueryHandler
        : IAppRequestHandler<GetRelatedProductsByProductIdQuery, ResultModel<List<ProductDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;
        private readonly IMediaStorageStrategy _mediaStorageStrategy;

        public GetRelatedProductsByProductIdQueryHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext,
            IMediaStorageStrategy mediaStorageStrategy)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
            _mediaStorageStrategy = mediaStorageStrategy;
        }

        public async Task<ResultModel<List<ProductDto>>> Handle(GetRelatedProductsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var tenantId = _tenantContext.TenantId;

            var relationRepo = _unitOfWork.GetRepository<ProductRelation>();
            var mediaRepo = _unitOfWork.GetRepository<MediaAttachment>();

            // 1️⃣ دریافت محصولات مرتبط
            var relatedProductsQuery = relationRepo.Table
                .Include(r => r.RelatedProduct)
                    .ThenInclude(p => p.Translations)
                .Include(r => r.Product)
                    .ThenInclude(p => p.Product_ProductCategories)
                .Where(r =>
                    r.ProductId == request.ProductId &&
                    r.Product.WebSiteId == tenantId)
                .Select(r => r.RelatedProduct)
                .Where(p => p != null)
                .Distinct();

            var relatedProducts = await relatedProductsQuery.ToListAsync(cancellationToken);

            if (!relatedProducts.Any())
                return ResultModel<List<ProductDto>>.Fail("هیچ محصول مرتبطی یافت نشد.");

            // 2️⃣ گرفتن تصاویر از MediaAttachment
            var productIds = relatedProducts.Select(p => p.Id).ToList();

            var attachments = await mediaRepo.GetAllAsync(
                                            predicate: x => productIds.Contains(x.EntityId) && x.EntityType == "Product" && x.TenantId == tenantId,
                                            func: x => x.Include(y => y.MediaFile));

            // 3️⃣ ساخت DTO نهایی
            var productDtos = relatedProducts.Select(p =>
            {
                var translation = p.Translations.FirstOrDefault();

                // آدرس تصاویر محصول
                var imageUrls = attachments
                    .Where(a => a.EntityId == p.Id)
                    .OrderByDescending(a => a.Purpose == "Main")
                    .Select(a => a.MediaFile != null
                        ? _mediaStorageStrategy.GetUrl(a.MediaFile.Key)
                        : string.Empty)
                    .Where(url => !string.IsNullOrEmpty(url))
                    .ToList();

                return new ProductDto
                {
                    Id = p.Id,
                    SKU = p.SKU,
                    Name = translation?.Name ?? string.Empty,
                    Description = translation?.Description ?? string.Empty,
                    WebSiteLanguageId = translation?.WebSiteLanguageId ?? 0,
                    ImageUrls = imageUrls
                };
            }).ToList();

            return ResultModel<List<ProductDto>>.Success(productDtos);
        }
    }
}
