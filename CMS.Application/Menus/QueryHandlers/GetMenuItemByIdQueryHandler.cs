using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Menus.Queries;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Navigation.Entities;

namespace CMS.Application.Blog.QueryHandlers
{
    public class GetMenuItemByIdQueryHandler : IAppRequestHandler<GetMenuItemByIdQuery, ResultModel<MenuItem?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetMenuItemByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<MenuItem?>> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetRepository<MenuItem>().GetAsync(
                                                predicate: x => x.Id == request.Id && x.Menu.WebSiteId == _tenantContext.TenantId);

        }
    }
}
