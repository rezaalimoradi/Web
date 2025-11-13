using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Media;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Media.Entities;
using Microsoft.AspNetCore.Http;

namespace CMS.Application.Catalog.CommandHandlers
{
    public class CreateProductCategoryCommandHandler
        : IAppRequestHandler<CreateProductCategoryCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;
        private readonly IMediaStorageStrategy _mediaStorageStrategy;

        public CreateProductCategoryCommandHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext,
            IMediaStorageStrategy mediaStorageStrategy)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
            _mediaStorageStrategy = mediaStorageStrategy;
        }

        public async Task<ResultModel<long>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var tenantId = _tenantContext.TenantId;

            try
            {
                // 1. ساخت دسته محصول
                var category = ProductCategory.Create(tenantId);

                // درست: دیگه Id نمی‌فرستیم! فقط languageId و محتوا
                category.AddTranslation(
                    languageId: request.WebSiteLanguageId,
                    title: request.Title,
                    description: request.Description,
                    slug: request.Slug,
                    metaTitle: request.MetaTitle,
                    metaDescription: request.MetaDescription,
                    metaKeywords: request.MetaKeywords,
                    canonicalUrl: request.CanonicalUrl
                );

                // 2. تنظیم والد
                if (request.ParentId.HasValue)
                {
                    var parent = await _unitOfWork.GetRepository<ProductCategory>()
                        .GetByIdAsync(request.ParentId.Value);

                    if (parent == null)
                        return ResultModel<long>.Fail("Parent category not found.");

                    if (parent.WebSiteId != tenantId)
                        return ResultModel<long>.Fail("Parent category belongs to another tenant.");

                    parent.AddChild(category);
                }

                // 3. ذخیره اولیه برای گرفتن Id
                await _unitOfWork.GetRepository<ProductCategory>().InsertAsync(category);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // 4. آپلود و اتصال تصاویر
                if (request.ImageFiles != null && request.ImageFiles.Any())
                {
                    await UploadAndAttachImagesAsync(category, request.ImageFiles, tenantId, cancellationToken);
                }

                return ResultModel<long>.Success(category.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return ResultModel<long>.Fail("An unexpected error occurred: " + ex.Message);
            }
        }

        private async Task UploadAndAttachImagesAsync(
    ProductCategory category,
    IEnumerable<IFormFile> files,
    long tenantId,
    CancellationToken cancellationToken)
        {
            var container = $"tenant-{tenantId}/categories";
            var mediaFiles = new List<MediaFile>();
            var isFirst = true;

            // 1. آپلود همه فایل‌ها
            foreach (var file in files)
            {
                var key = await _mediaStorageStrategy.UploadAsync(file, container);

                var mediaFile = new MediaFile(
                    key: key,
                    fileName: file.FileName,
                    contentType: file.ContentType,
                    sizeInBytes: file.Length,
                    mediaType: "Image",
                    tenantId: tenantId,
                    languageId: _tenantContext.CurrentLanguageId
                );

                mediaFiles.Add(mediaFile);
                await _unitOfWork.GetRepository<MediaFile>().InsertAsync(mediaFile);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 2. ساخت Attachment با EntityId و EntityType واقعی
            foreach (var mediaFile in mediaFiles)
            {
                var attachment = new MediaAttachment(
                    tenantId: tenantId,
                    mediaFileId: mediaFile.Id,
                    entityId: category.Id,                    // درست: Id واقعی
                    entityType: nameof(ProductCategory)       // درست: نوع واقعی
                );

                if (isFirst)
                {
                    attachment.SetPurpose("Main");
                    category.AddMainImage(attachment);
                }
                else
                {
                    attachment.SetPurpose("Gallery");
                    attachment.SetDisplayOrder(mediaFiles.IndexOf(mediaFile)); // یا یه شمارنده
                    category.AddGalleryImage(attachment);
                }

                await _unitOfWork.GetRepository<MediaAttachment>().InsertAsync(attachment);
                isFirst = false;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}