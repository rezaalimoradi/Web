using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetAllExistsProductAttributeByProductIdQueryHandler
        : IAppRequestHandler<GetAllExistsProductAttributeByProductIdQuery, ResultModel<IPagedList<ProductAttribute>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllExistsProductAttributeByProductIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<ProductAttribute>>> Handle(
    GetAllExistsProductAttributeByProductIdQuery request,
    CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<ProductProductAttribute>().GetAllPagedAsync(
                predicate: x => x.ProductId == request.ProductId &&
                                x.Product.WebSiteId == _tenantContext.TenantId,
                func: x => x.Include(y => y.ProductAttribute)
                            .ThenInclude(z => z.Translations),
                pageIndex: request.Page,
                pageSize: request.PageSize);

            var mapped = new PagedList<ProductAttribute>(
                result.Select(x => x.ProductAttribute).ToList(),
                result.PageIndex,
                result.PageSize,
                result.TotalCount
            );

            return ResultModel<IPagedList<ProductAttribute>>.Success(mapped);
        }

    }
}
