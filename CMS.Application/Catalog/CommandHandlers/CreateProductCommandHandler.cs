using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Media;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Media.Entities;

namespace CMS.Application.Catalog.CommandHandlers
{
    public class CreateProductCommandHandler
        : IAppRequestHandler<CreateProductCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;
        private readonly IMediaStorageStrategy _mediaStorageStrategy;

        public CreateProductCommandHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext,
            IMediaStorageStrategy mediaStorageStrategy)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
            _mediaStorageStrategy = mediaStorageStrategy ?? throw new ArgumentNullException(nameof(mediaStorageStrategy));
        }
        public async Task<ResultModel<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var tenantId = _tenantContext.TenantId;

            if (request.CategoryIds == null || !request.CategoryIds.Any())
                return ResultModel<long>.Fail("حداقل یک دسته‌بندی انتخاب کنید.");

            try
            {
                // 1. اعتبارسنجی دسته‌ها
                var categoryRepo = _unitOfWork.GetRepository<ProductCategory>();
                var validCategories = await categoryRepo.GetAllAsync(
                    c => request.CategoryIds.Contains(c.Id) && c.WebSiteId == tenantId
                );

                var invalidIds = request.CategoryIds.Except(validCategories.Select(c => c.Id)).ToList();
                if (invalidIds.Any())
                    return ResultModel<long>.Fail($"دسته‌بندی‌های نامعتبر: {string.Join(", ", invalidIds)}");

                // 2. ایجاد محصول
                var product = new Product(
                    price: request.Price,
                    sku: request.SKU,
                    type: request.Type,
                    isOriginal: request.IsOriginal,
                    width: request.Width,
                    height: request.Height,
                    length: request.Length,
                    websiteId: tenantId,
                    brandId: request.BrandId,
                    taxId: request.TaxId,
                    specialPrice: request.SpecialPrice,
                    specialPriceStart: request.SpecialPriceStart,
                    specialPriceEnd: request.SpecialPriceEnd,
                    isPublished: request.IsPublished,
                    showOnHomepage: request.ShowOnHomepage,
                    allowCustomerReviews: request.AllowCustomerReviews,
                    isCallForPrice: request.IsCallForPrice,
                    stockQuantity: request.StockQuantity,
                    manageInventory: request.ManageInventory,
                    barcode: request.Barcode
                );

                product.AddTranslation(
                    languageId: request.WebSiteLanguageId,
                    name: request.Name,
                    description: request.Description,
                    slug: request.Slug
                );

                // 3. ذخیره محصول (برای گرفتن Id)
                var productRepo = _unitOfWork.GetRepository<Product>();
                await productRepo.InsertAsync(product);
                await _unitOfWork.SaveChangesAsync(cancellationToken); // مهم: Id تولید شد

                // 4. ایجاد مپینگ‌ها (حالا Product.Id معتبره)
                var mappingRepo = _unitOfWork.GetRepository<Product_ProductCategory_Mapping>();
                foreach (var category in validCategories)
                {
                    var mapping = new Product_ProductCategory_Mapping
                    {
                        ProductId = product.Id,        // حالا معتبره
                        ProductCategoryId = category.Id
                    };
                    await mappingRepo.InsertAsync(mapping);
                }

                // 5. پردازش تصاویر
                if (request.ImageFiles?.Any() == true)
                {
                    var container = $"tenant-{tenantId}/products";
                    var mediaFileRepo = _unitOfWork.GetRepository<MediaFile>();
                    var attachmentRepo = _unitOfWork.GetRepository<MediaAttachment>();
                    bool isFirst = true;

                    foreach (var file in request.ImageFiles)
                    {
                        var key = await _mediaStorageStrategy.UploadAsync(file, container);

                        var mediaFile = new MediaFile(
                            key: key,
                            fileName: file.FileName,
                            contentType: file.ContentType,
                            sizeInBytes: file.Length,
                            mediaType: "Image",
                            tenantId: tenantId,
                            languageId: request.WebSiteLanguageId
                        );

                        await mediaFileRepo.InsertAsync(mediaFile);
                        await _unitOfWork.SaveChangesAsync(cancellationToken); // Id رسانه

                        var attachment = new MediaAttachment(
                            tenantId: tenantId,
                            mediaFileId: mediaFile.Id,
                            entityId: product.Id,
                            entityType: "Product",
                            purpose: isFirst ? "Main" : "Gallery"
                        );

                        await attachmentRepo.InsertAsync(attachment);
                        isFirst = false;
                    }
                }

                // 6. ذخیره نهایی
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(product.Id);
            }
            catch (Exception ex)
            {
                // لاگ کن
                return ResultModel<long>.Fail("خطا در ایجاد محصول: " + ex.Message);
            }
        }
    }
}