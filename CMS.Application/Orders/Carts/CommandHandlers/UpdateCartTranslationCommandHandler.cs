using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.CommandHandlers
{
    public class UpdateCartTranslationCommandHandler : IAppRequestHandler<UpdateCartTranslationCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateCartTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<bool>> Handle(UpdateCartTranslationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Cart>();

            var cart = await repo.GetAsync(
                predicate: x => x.Id == request.CartId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(c => c.Translations)
            );

            if (cart == null)
                return ResultModel<bool>.Fail("Cart not found.");

            try
            {
                cart.UpdateTranslation(request.LanguageId, request.Title, request.Description);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<bool>.Success(true);
            }
            catch (DomainException ex)
            {
                return ResultModel<bool>.Fail(ex.Message);
            }
        }
    }
}
