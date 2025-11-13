using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetProductCategoryMappingsQueryHandler : IAppRequestHandler<GetProductCategoryMappingsQuery, ResultModel<IPagedList<Product_ProductCategory_Mapping>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductCategoryMappingsQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<Product_ProductCategory_Mapping>>> Handle(GetProductCategoryMappingsQuery request, CancellationToken cancellationToken)
        {

            var result = await _unitOfWork.GetRepository<Product_ProductCategory_Mapping>().GetAllPagedAsync(
                                                        predicate: x => x.Product.WebSiteId == _tenantContext.TenantId 
                                                        && x.Product.Id == request.ProductId,
                                                        pageIndex: request.Page,
                                                        pageSize: request.PageSize);

            return ResultModel<IPagedList<Product_ProductCategory_Mapping>>.Success(result);
        }
    }
}
