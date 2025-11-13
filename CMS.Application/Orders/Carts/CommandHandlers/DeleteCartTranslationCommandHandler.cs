using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.CommandHandlers
{
    public class DeleteCartTranslationCommandHandler : IAppRequestHandler<DeleteCartTranslationCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteCartTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<bool>> Handle(DeleteCartTranslationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Cart>();

            var cart = await repo.GetAsync(
                predicate: x => x.Id == request.CartId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(c => c.Translations)
            );

            if (cart == null)
                return ResultModel<bool>.Fail("Cart not found.");

            cart.RemoveTranslation(request.LanguageId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel<bool>.Success(true);
        }
    }

}
