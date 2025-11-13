using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Menus.Queries;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Navigation.Entities;

namespace CMS.Application.Menus.QueryHandlers
{
    public class GetAllMenusQueryHandler : IAppRequestHandler<GetAllMenusQuery, ResultModel<IPagedList<Menu>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllMenusQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<Menu>>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<Menu>().GetAllPagedAsync(
                                                                    predicate: x => x.WebSiteId == _tenantContext.TenantId,
                                                                    pageIndex: request.Page,
                                                                    pageSize: request.PageSize);

            return ResultModel<IPagedList<Menu>>.Success(result);
        }
    }
}
