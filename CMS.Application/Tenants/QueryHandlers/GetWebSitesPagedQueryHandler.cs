using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants.Dtos;
using CMS.Application.Tenants.Queries;
using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Application.Tenants.QueryHandlers
{
    public class GetWebSitesPagedQueryHandler : IAppRequestHandler<GetWebSitesPagedQuery, IPagedList<WebSite>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetWebSitesPagedQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IPagedList<WebSite>> Handle(GetWebSitesPagedQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetRepository<WebSite>().GetAllPagedAsync(
                                                                predicate: x => (string.IsNullOrEmpty(request.SearchQuery) || x.CompanyName.Contains(request.SearchQuery)),
                                                                func: null,
                                                                pageIndex: request.Page,
                                                                pageSize: request.PageSize);
        }
    }
}
