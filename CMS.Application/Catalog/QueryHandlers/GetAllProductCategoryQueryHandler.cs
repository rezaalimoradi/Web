using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetAllProductCategoryQueryHandler : IAppRequestHandler<GetAllProductCategoryQuery, ResultModel<IPagedList<ProductCategory>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllProductCategoryQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<ProductCategory>>> Handle(GetAllProductCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<ProductCategory>().GetAllPagedAsync(
                predicate: x => x.WebSiteId == _tenantContext.TenantId,
                func: query => query
                    .Include(c => c.Translations)          
                    .Include(c => c.MediaAttachments)     
                        .ThenInclude(ma => ma.MediaFile), 
                pageIndex: request.Page,
                pageSize: request.PageSize
            );

            return ResultModel<IPagedList<ProductCategory>>.Success(result);
        }
    }
}
