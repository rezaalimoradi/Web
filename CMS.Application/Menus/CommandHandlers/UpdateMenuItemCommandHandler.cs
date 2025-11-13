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
    public class UpdateMenuItemCommandHandler : IAppRequestHandler<UpdateMenuItemCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateMenuItemCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menu = await _unitOfWork.GetRepository<Menu>().GetAsync(
                                                        predicate: x => x.Id == request.MenuId && x.WebSiteId == _tenantContext.TenantId,
                                                        include: x => x.Include(y => y.Items).ThenInclude(y => y.Translations));
            if (menu == null)
            {
                return ResultModel<long>.Fail("MenuItem not found");
            }

            try
            {
                var menuItemObj = new MenuItem(
                    menuId: request.MenuId,
                    parentId: request.ParentId,
                    isActive: request.IsActive,
                    displayOrder: request.DisplayOrder
                );

                menuItemObj.UpdateTranslation(
                    request.WebSiteLanguageId,
                    request.Title,
                    request.Link
                );

                menuItemObj.SetParent(menuItemObj);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(menu.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
