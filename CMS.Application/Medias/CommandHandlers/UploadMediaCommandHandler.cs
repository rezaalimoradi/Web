using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Media;
using CMS.Application.Medias.Commands;
using CMS.Application.Medias.Dtos;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Media.Entities;

namespace CMS.Application.Medias.CommandHandlers
{
    public class UploadMediaCommandHandler
            : IAppRequestHandler<UploadMediaCommand, ResultModel<MediaFileDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;
        private readonly IMediaStorageStrategy _storageFactory;

        public UploadMediaCommandHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext,
            IMediaStorageStrategy storageFactory
        )
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
            _storageFactory = storageFactory;
        }

        public async Task<ResultModel<MediaFileDto>> Handle(UploadMediaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.File == null)
                    return ResultModel<MediaFileDto>.Fail("File cannot be null.");

                var tenantId = _tenantContext.TenantId;
                var langId = _tenantContext.CurrentLanguageId;

                var strategy = _storageFactory;
                var container = $"tenant-{tenantId}/{request.EntityType.ToLower()}";
                var key = await strategy.UploadAsync(request.File, container);
                var mediaType = request.File.ContentType.StartsWith("image") ? "Image" : "File";

                var mediaFile = new MediaFile(
                    key: key,
                    fileName: request.File.FileName,
                    contentType: request.File.ContentType,
                    sizeInBytes: request.File.Length,
                    mediaType: mediaType,
                    tenantId: tenantId,
                    languageId: langId
                );

                await _unitOfWork.GetRepository<MediaFile>().InsertAsync(mediaFile);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var attachment = new MediaAttachment(
                    mediaFileId: mediaFile.Id,
                    entityId: request.EntityId,
                    entityType: request.EntityType,
                    purpose: request.Purpose,
                    tenantId: tenantId
                );

                await _unitOfWork.GetRepository<MediaAttachment>().InsertAsync(attachment);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var dto = new MediaFileDto
                {
                    Id = mediaFile.Id,
                    FileName = mediaFile.FileName,
                    ContentType = mediaFile.ContentType,
                    SizeInBytes = mediaFile.SizeInBytes,
                    MediaType = mediaFile.MediaType,
                    Url = strategy.GetUrl(mediaFile.Key),
                    UploadedAt = mediaFile.UploadedAt
                };

                return ResultModel<MediaFileDto>.Success(dto);
            }
            catch (DomainException ex)
            {
                return ResultModel<MediaFileDto>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return ResultModel<MediaFileDto>.Fail("خطا در آپلود فایل: " + ex.Message);
            }
        }
    }
}
