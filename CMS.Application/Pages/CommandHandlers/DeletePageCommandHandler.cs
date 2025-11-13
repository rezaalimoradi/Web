using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Pages.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Pages.Entities;

namespace CMS.Application.Pages.CommandHandlers
{
    public class DeletePageCommandHandler : IAppRequestHandler<DeletePageCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeletePageCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(DeletePageCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Page>();
            var page = await _unitOfWork.GetRepository<Page>().GetAsync(
                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId);

            if (page == null || page.WebSiteId != _tenantContext.TenantId)
                return ResultModel.Fail("Page not found.");

            repo.Delete(page);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel.Success();
        }
    }
}
