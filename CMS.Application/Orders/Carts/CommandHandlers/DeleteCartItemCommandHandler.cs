using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Orders.Carts.Dtos;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.CommandHandlers
{
    public class DeleteCartItemCommandHandler
        : IAppRequestHandler<DeleteCartItemCommand, ResultModel<CartUpdateResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCartItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ResultModel<CartUpdateResultDto>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartRepo = _unitOfWork.GetRepository<Cart>();

            // دریافت سبد با آیتم‌ها
            var cart = await cartRepo.Table
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerIdentifier == request.CustomerIdentifier, cancellationToken);

            if (cart == null)
                return ResultModel<CartUpdateResultDto>.Fail("سبد خرید یافت نشد.");

            // حذف آیتم با استفاده از متد Aggregate
            try
            {
                cart.RemoveItem(request.ProductId);
            }
            catch (DomainException ex)
            {
                return ResultModel<CartUpdateResultDto>.Fail(ex.Message);
            }

            // بروزرسانی سبد
            cartRepo.Update(cart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // آماده‌سازی DTO برای UI
            var resultDto = new CartUpdateResultDto
            {
                ProductId = request.ProductId,
                Quantity = 0,
                ItemTotal = 0,
                TotalPrice = cart.TotalPrice
            };

            return ResultModel<CartUpdateResultDto>.Success(resultDto);
        }
    }
}
