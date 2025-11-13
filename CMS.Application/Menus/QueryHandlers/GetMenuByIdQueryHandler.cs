using CMS.Application.Blog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Navigation.Entities;

namespace CMS.Application.Blog.QueryHandlers
{
    public class GetMenuByIdQueryHandler : IAppRequestHandler<GetMenuByIdQuery, ResultModel<Menu?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetMenuByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<Menu?>> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetRepository<Menu>().GetAsync(
                                                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId);

        }
    }
}
