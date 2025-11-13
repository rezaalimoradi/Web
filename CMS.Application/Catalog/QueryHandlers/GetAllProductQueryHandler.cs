using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetAllProductQueryHandler : IAppRequestHandler<GetAllProductQuery, ResultModel<IPagedList<Product>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllProductQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<Product>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<Product>().GetAllPagedAsync(
                                                                    predicate: x => x.WebSiteId == _tenantContext.TenantId,
                                                                    func: x => x.Include(y => y.Translations),
                                                                    pageIndex: request.Page,
                                                                    pageSize: request.PageSize);

            return ResultModel<IPagedList<Product>>.Success(result);
        }
    }
}
