using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetAllBrandTranslationByBrandIdQueryHandler : IAppRequestHandler<GetAllBrandTranslationByBrandIdQuery, ResultModel<IPagedList<BrandTranslation>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetAllBrandTranslationByBrandIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<BrandTranslation>>> Handle(GetAllBrandTranslationByBrandIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetRepository<BrandTranslation>().GetAllPagedAsync(
                                                                    predicate: x => x.Brand.WebSiteId == _tenantContext.TenantId,
                                                                    func: x => x.Where(y => y.BrandId == request.BrandId),
                                                                    pageIndex: request.Page,
                                                                    pageSize: request.PageSize);

            return ResultModel<IPagedList<BrandTranslation>>.Success(result);
        }
    }
}
