using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Menus.Commands;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Navigation.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Menus.CommandHandlers
{
    public class CreateMenuItemTranslationCommandHandler : IAppRequestHandler<CreateMenuItemTranslationCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateMenuItemTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateMenuItemTranslationCommand request, CancellationToken cancellationToken)
        {
            var menu = await _unitOfWork.GetRepository<Menu>().GetAsync(
                                                        predicate: x => x.Id == request.MenuId && x.WebSiteId == _tenantContext.TenantId,
                                                        include: x => x.Include(y => y.Items).ThenInclude(y => y.Translations));
            if (menu == null)
            {
                return ResultModel<long>.Fail("Menu not found");
            }

            var menuitem = menu.Items.FirstOrDefault(x => x.Id == request.MenuItemId);
            if (menuitem == null)
            {
                return ResultModel<long>.Fail("MenuItem not found.");
            }

            try
            {
                var menuItemTranslation = menuitem.AddTranslation(
                    webSiteLanguageId: request.WebSiteLanguageId,
                    title: request.Title,
                    link: request.Link);

                var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return ResultModel<long>.Success(menuItemTranslation.Id);
                }

                return ResultModel<long>.Fail("MenuItemTranslation cration failed");
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
