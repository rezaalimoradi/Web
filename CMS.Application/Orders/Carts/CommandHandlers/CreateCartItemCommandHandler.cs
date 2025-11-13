using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.CommandHandlers
{
    public class CreateCartItemCommandHandler : IAppRequestHandler<CreateCartItemCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateCartItemCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {
            // اعتبارسنجی ورودی‌ها
            var validationResult = ValidateRequest(request);
            if (!validationResult.IsSuccess)
                return validationResult;

            var cartRepository = _unitOfWork.GetRepository<Cart>();
            var cart = await GetCartAsync(cartRepository, request.CartId, cancellationToken);
            if (cart is null)
                return ResultModel<long>.Fail("Cart not found");

            try
            {
                cart.AddItem(request.ProductId, request.Quantity, request.UnitPrice, request.Discount);

                cartRepository.Update(cart);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var newItem = cart.Items.Single(i => i.ProductId == request.ProductId);
                return ResultModel<long>.Success(newItem.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }

        private ResultModel<long> ValidateRequest(CreateCartItemCommand request)
        {
            if (request.Quantity <= 0)
                return ResultModel<long>.Fail("Quantity must be greater than 0");
            if (request.UnitPrice < 0)
                return ResultModel<long>.Fail("UnitPrice cannot be negative");
            if (request.Discount < 0 || request.Discount > request.UnitPrice)
                return ResultModel<long>.Fail("Invalid Discount value");

            return ResultModel<long>.Success(0); // موفقیت موقت
        }

        private async Task<Cart?> GetCartAsync(IRepository<Cart> repository, long cartId, CancellationToken cancellationToken)
        {
            return await repository.GetAsync(
                predicate: x => x.Id == cartId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(c => c.Items)
            );
        }
    }
}
