using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetAllProductRelationByProductIdQueryHandler
        : IAppRequestHandler<GetAllProductRelationByProductIdQuery, ResultModel<IPagedList<ProductRelation>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllProductRelationByProductIdQueryHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<ProductRelation>>> Handle(
            GetAllProductRelationByProductIdQuery request,
            CancellationToken cancellationToken)
        {
            var relationRepo = _unitOfWork.GetRepository<ProductRelation>();

            var result = await relationRepo.GetAllPagedAsync(
                predicate: x =>
                    x.ProductId == request.ProductId &&
                    x.Product.WebSiteId == _tenantContext.TenantId,
                func: q => q
                    .Include(r => r.RelatedProduct)
                        .ThenInclude(p => p.Translations)
                    .Include(r => r.RelatedProduct)
                        .ThenInclude(p => p.Product_ProductCategories),
                pageIndex: request.Page,
                pageSize: request.PageSize
            );

            if (result == null)
            {
                var emptyPagedList = new PagedList<ProductRelation>(
                    new List<ProductRelation>(),
                    request.Page,
                    request.PageSize,
                    0
                );
                return ResultModel<IPagedList<ProductRelation>>.Success(emptyPagedList);
            }

            return ResultModel<IPagedList<ProductRelation>>.Success(result);
        }
    }
}
