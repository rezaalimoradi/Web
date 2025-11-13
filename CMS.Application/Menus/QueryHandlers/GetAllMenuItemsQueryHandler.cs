using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Menus.Queries;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Navigation.Entities;

namespace CMS.Application.Menus.QueryHandlers
{
    public class GetAllMenuItemsQueryHandler : IAppRequestHandler<GetAllMenuItemsQuery, ResultModel<IPagedList<MenuItem>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllMenuItemsQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<MenuItem>>> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<MenuItem>().GetAllPagedAsync(
                                                                    predicate: x => x.Menu.WebSiteId == _tenantContext.TenantId,
                                                                    pageIndex: request.Page,
                                                                    pageSize: request.PageSize);

            return ResultModel<IPagedList<MenuItem>>.Success(result);
        }
    }
}
