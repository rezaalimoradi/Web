using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Menus.Queries;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Navigation.Entities;

namespace CMS.Application.Blog.QueryHandlers
{
    public class GetMenuItemTranslationByIdQueryHandler : IAppRequestHandler<GetMenuItemTranslationByIdQuery, ResultModel<MenuItemTranslation?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetMenuItemTranslationByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<MenuItemTranslation?>> Handle(GetMenuItemTranslationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetRepository<MenuItemTranslation>().GetAsync(
                                                predicate: x => x.Id == request.Id);

        }
    }
}
