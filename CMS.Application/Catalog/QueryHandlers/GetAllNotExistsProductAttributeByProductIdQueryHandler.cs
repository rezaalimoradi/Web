using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetAllNotExistsProductAttributeByProductIdQueryHandler
        : IAppRequestHandler<GetAllNotExistsProductAttributeByProductIdQuery, ResultModel<IPagedList<ProductAttribute>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllNotExistsProductAttributeByProductIdQueryHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<ProductAttribute>>> Handle(
            GetAllNotExistsProductAttributeByProductIdQuery request,
            CancellationToken cancellationToken)
        {
            var assignedAttributeIds = (await _unitOfWork
                .GetRepository<ProductProductAttribute>()
                .GetAllAsync(pp => pp.ProductId == request.ProductId))
                .Select(pp => pp.ProductAttributeId)
                .ToList();

            var unassignedPaged = await _unitOfWork
                .GetRepository<ProductAttribute>()
                .GetAllPagedAsync(
                    predicate: a =>
                        a.WebSiteId == _tenantContext.TenantId &&
                        !assignedAttributeIds.Contains(a.Id),
                    func: q => q.Include(a => a.Translations),
                    pageIndex: request.Page,
                    pageSize: request.PageSize
                );

            return ResultModel<IPagedList<ProductAttribute>>.Success(unassignedPaged);
        }
    }
}
