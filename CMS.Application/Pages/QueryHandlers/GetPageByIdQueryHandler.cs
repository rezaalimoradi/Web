using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Pages.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Pages.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Pages.QueryHandlers
{
    public class GetPageByIdQueryHandler : IAppRequestHandler<GetPageByIdQuery, ResultModel<Page?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetPageByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<Page?>> Handle(GetPageByIdQuery request, CancellationToken cancellationToken)
        {
            var page = await _unitOfWork.GetRepository<Page>().GetAsync(
                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(p => p.Translations));

            if (page == null)
                return ResultModel<Page?>.Fail("Page not found or access denied.");

            return ResultModel<Page?>.Success(page);
        }
    }
}
