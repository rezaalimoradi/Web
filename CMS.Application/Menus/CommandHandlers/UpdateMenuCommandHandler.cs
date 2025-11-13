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
    public class UpdateMenuCommandHandler : IAppRequestHandler<UpdateMenuCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateMenuCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _unitOfWork.GetRepository<Menu>().GetAsync(
                                            predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                                            include: x => x.Include(y => y.Items).ThenInclude(y => y.Translations));
            if (menu == null)
            {
                return ResultModel<long>.Fail("Menu not found");
            }

            try
            {
                _unitOfWork.GetRepository<Menu>().Update(menu);

                var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return ResultModel<long>.Success(menu.Id);
                }

                return ResultModel<long>.Fail("Menu cration failed");
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
