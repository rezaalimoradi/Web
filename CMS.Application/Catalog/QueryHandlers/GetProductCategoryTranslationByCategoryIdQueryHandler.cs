using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetProductCategoryTranslationByCategoryIdQueryHandler : IAppRequestHandler<GetProductCategoryTranslationByCategoryIdQuery, ResultModel<IPagedList<ProductCategoryTranslation>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductCategoryTranslationByCategoryIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<ProductCategoryTranslation>>> Handle(GetProductCategoryTranslationByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<ProductCategoryTranslation>().GetAllPagedAsync(
                                                                    predicate: x => x.ProductCategory.WebSiteId == _tenantContext.TenantId,
                                                                    func: x => x.Where(y => y.ProductCategoryId == request.CategoryId),
                                                                    pageIndex: request.Page,
                                                                    pageSize: request.PageSize);

            return ResultModel<IPagedList<ProductCategoryTranslation>>.Success(result);
        }
    }
}
