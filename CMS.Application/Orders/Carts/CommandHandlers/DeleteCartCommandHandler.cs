using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Commands;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;

namespace CMS.Application.Orders.Carts.CommandHandlers
{
    public class DeleteCartCommandHandler : IAppRequestHandler<DeleteCartCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<bool>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Cart>();
            var cart = await repo.GetByIdAsync(request.CartId);

            if (cart is null)
                return ResultModel<bool>.Fail("سبد پیدا نشد.");

            repo.Delete(cart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel<bool>.Success(true);
        }
    }
}
