using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Orders.Carts.Dtos;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.CommandHandlers
{
    public class UpdateCartItemCommandHandler : IAppRequestHandler<UpdateCartItemCommand, ResultModel<CartUpdateResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateCartItemCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
        }

        public async Task<ResultModel<CartUpdateResultDto>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                return ResultModel<CartUpdateResultDto>.Fail("درخواست نامعتبر است.");

            if (request.Quantity <= 0)
                return ResultModel<CartUpdateResultDto>.Fail("تعداد باید بیشتر از صفر باشد.");

            var tenantId = _tenantContext.TenantId;
            var customerIdentifier = !string.IsNullOrWhiteSpace(request.CustomerIdentifier)
                ? request.CustomerIdentifier
                : _tenantContext.CustomerIdentifier ?? "guest";

            var cartRepo = _unitOfWork.GetRepository<Cart>();
            var cart = await cartRepo.Table
                .Include(c => c.Items)
                .FirstOrDefaultAsync(
                    c => c.WebSiteId == tenantId && c.CustomerIdentifier == customerIdentifier,
                    cancellationToken
                );

            if (cart == null)
                return ResultModel<CartUpdateResultDto>.Fail("سبد خرید یافت نشد.");

            var item = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (item == null)
                return ResultModel<CartUpdateResultDto>.Fail("محصول مورد نظر در سبد خرید وجود ندارد.");

            try
            {
                item.UpdateQuantity(request.Quantity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // 📦 DTO خروجی به سمت فرانت
                var dto = new CartUpdateResultDto
                {
                    CartId = cart.Id,
                    ItemId = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ItemTotal = item.TotalPrice, // یا (item.UnitPrice - item.Discount) * item.Quantity
                    CartTotal = cart.Items.Sum(i => i.TotalPrice)
                };

                return ResultModel<CartUpdateResultDto>.Success(dto);
            }
            catch (DomainException ex)
            {
                return ResultModel<CartUpdateResultDto>.Fail(ex.Message);
            }
            catch (Exception)
            {
                return ResultModel<CartUpdateResultDto>.Fail("خطایی در به‌روزرسانی سبد خرید رخ داده است.");
            }
        }
    }
}
