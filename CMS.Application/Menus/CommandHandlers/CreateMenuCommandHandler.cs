using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Menus.Commands;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Navigation.Entities;

namespace CMS.Application.Menus.CommandHandlers
{
    public class CreateMenuCommandHandler : IAppRequestHandler<CreateMenuCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateMenuCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var menu = new Menu(_tenantContext.TenantId, request.Name);

                await _unitOfWork.GetRepository<Menu>().InsertAsync(menu);

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
