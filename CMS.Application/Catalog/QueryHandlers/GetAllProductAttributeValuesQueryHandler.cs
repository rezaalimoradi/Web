using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetAllProductAttributeValuesQueryHandler : IAppRequestHandler<GetAllProductAttributeValuesQuery, ResultModel<IPagedList<ProductAttributeValue>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllProductAttributeValuesQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<ProductAttributeValue>>> Handle(GetAllProductAttributeValuesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<ProductAttributeValue>().GetAllPagedAsync(
                                                                    predicate: x => x.ProductAttribute.Id == request.ProductAttributeId && x.ProductAttribute.WebSiteId == _tenantContext.TenantId,
                                                                    func: x => x.Include(y => y.Translations),
                                                                    pageIndex: request.Page,
                                                                    pageSize: request.PageSize);

            return ResultModel<IPagedList<ProductAttributeValue>>.Success(result);
        }
    }
}
