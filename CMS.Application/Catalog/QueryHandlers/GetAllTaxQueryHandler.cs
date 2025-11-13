using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetAllTaxQueryHandler : IAppRequestHandler<GetAllTaxQuery, ResultModel<IPagedList<Tax>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllTaxQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<Tax>>> Handle(GetAllTaxQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<Tax>().GetAllPagedAsync(
                                                                    predicate: x => x.WebSiteId == _tenantContext.TenantId,
                                                                    func: x => x.Include(y => y.Translations),
                                                                    pageIndex: request.Page,
                                                                    pageSize: request.PageSize);

            return ResultModel<IPagedList<Tax>>.Success(result);
        }
    }
}
