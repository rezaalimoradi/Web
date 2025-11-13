using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Media;
using CMS.Application.Medias.Commands;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Media.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Medias.CommandHandlers
{
    public class DeleteMediaCommandHandler
        : IAppRequestHandler<DeleteMediaCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;
        private readonly IMediaStorageStrategy _storageFactory;

        public DeleteMediaCommandHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext,
            IMediaStorageStrategy storageFactory
        )
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
            _storageFactory = storageFactory;
        }

        public async Task<ResultModel<bool>> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tenantId = _tenantContext.TenantId;

                var media = await _unitOfWork.GetRepository<MediaFile>()
                    .GetAsync(m => m.Id == request.MediaId && m.TenantId == tenantId);

                if (media == null)
                    return ResultModel<bool>.Fail("Media file not found.");

                var strategy = _storageFactory;
                await strategy.DeleteAsync(media.Key);

                _unitOfWork.GetRepository<MediaFile>().Delete(media);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<bool>.Success(true);
            }
            catch (DomainException ex)
            {
                return ResultModel<bool>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return ResultModel<bool>.Fail("خطا در حذف فایل: " + ex.Message);
            }
        }
    }
}
