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
    public class UpdateCartItemTranslationCommandHandler : IAppRequestHandler<UpdateCartItemTranslationCommand, ResultModel<CartUpdateResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateCartItemTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
        }

        public async Task<ResultModel<CartUpdateResultDto>> Handle(UpdateCartItemTranslationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Cart>();

            var cart = await repo.GetAsync(
                predicate: x => x.Id == request.CartId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(c => c.Items).ThenInclude(i => i.Translations)
            );

            if (cart is null)
                return ResultModel<CartUpdateResultDto>.Fail("سبد خرید یافت نشد.");

            var item = cart.Items.FirstOrDefault(i => i.Id == request.CartItemId);
            if (item is null)
                return ResultModel<CartUpdateResultDto>.Fail("آیتم مورد نظر در سبد خرید یافت نشد.");

            try
            {
                // ✏️ به‌روزرسانی ترجمه عنوان محصول
                item.UpdateTranslation(request.LanguageId, request.Title);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // ✅ DTO خروجی مشابه سایر هندلرها
                var dto = new CartUpdateResultDto
                {
                    CartId = cart.Id,
                    ItemId = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ItemTotal = item.TotalPrice,
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
                return ResultModel<CartUpdateResultDto>.Fail("خطایی در به‌روزرسانی ترجمه آیتم رخ داد.");
            }
        }
    }
}
