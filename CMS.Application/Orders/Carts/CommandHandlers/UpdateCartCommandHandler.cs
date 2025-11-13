using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Commands;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Orders.Carts;

namespace CMS.Application.Orders.Carts.CommandHandlers
{
    public class UpdateCartCommandHandler : IAppRequestHandler<UpdateCartCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ResultModel<bool>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            if (request.CartId <= 0)
                return ResultModel<bool>.Fail("شناسه سبد خرید نامعتبر است.");

            var repo = _unitOfWork.GetRepository<Cart>();
            var cart = await repo.GetByIdAsync(request.CartId);

            if (cart == null)
                return ResultModel<bool>.Fail("سبد خرید یافت نشد.");

            try
            {
                // اگر CustomerIdentifier ارسال شده، فقط اون رو آپدیت کن
                if (!string.IsNullOrWhiteSpace(request.CustomerIdentifier))
                {
                    cart.UpdateCustomerIdentifier(request.CustomerIdentifier);
                }

                // اگر نیاز به آپدیت فیلدهای دیگر هست، اینجا اضافه کن
                // مثلا cart.Status = request.Status;

                repo.Update(cart);
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
