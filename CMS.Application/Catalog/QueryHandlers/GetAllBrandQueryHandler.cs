using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetAllBrandQueryHandler : IAppRequestHandler<GetAllBrandQuery, ResultModel<IPagedList<Brand>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllBrandQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<Brand>>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<Brand>().GetAllPagedAsync(
                                                                    predicate: x => x.WebSiteId == _tenantContext.TenantId,
                                                                    func: x => x.Include(y => y.Translations),
                                                                    pageIndex: request.Page,
                                                                    pageSize: request.PageSize);

            return ResultModel<IPagedList<Brand>>.Success(result);
        }
    }
}
