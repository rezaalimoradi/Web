using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetProductTranslationByProductIdQueryHandler : IAppRequestHandler<GetAllProductTranslationByProductIdQuery, ResultModel<IPagedList<ProductTranslation>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductTranslationByProductIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<ProductTranslation>>> Handle(GetAllProductTranslationByProductIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<ProductTranslation>().GetAllPagedAsync(
                                                                    predicate: x => x.Product.WebSiteId == _tenantContext.TenantId,
                                                                    func: x => x.Where(y => y.ProductId == request.ProductId),
                                                                    pageIndex: request.Page,
                                                                    pageSize: request.PageSize);

            return ResultModel<IPagedList<ProductTranslation>>.Success(result);
        }
    }
}
