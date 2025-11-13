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
    public class DeleteMenuItemCommandHandler : IAppRequestHandler<DeleteMenuItemCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteMenuItemCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await _unitOfWork.GetRepository<Menu>().GetAsync(
                                                        predicate: x => x.Items.FirstOrDefault().Id == request.MenuItemId && x.WebSiteId == _tenantContext.TenantId,
                                                        include: x => x.Include(y => y.Items).ThenInclude(y => y.Translations));
            if (menuItem == null)
            {
                return ResultModel<long>.Fail("MenuItem not found");
            }

            try
            {

                _unitOfWork.GetRepository<Menu>().Delete(menuItem);

                var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return ResultModel<long>.Success(menuItem.Id);
                }

                return ResultModel<long>.Fail("MenuItem cration failed");
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
