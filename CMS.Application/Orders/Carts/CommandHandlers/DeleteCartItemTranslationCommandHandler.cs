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
    public class DeleteCartItemTranslationCommandHandler
        : IAppRequestHandler<DeleteCartItemTranslationCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteCartItemTranslationCommandHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
        }

        public async Task<ResultModel<bool>> Handle(DeleteCartItemTranslationCommand request, CancellationToken cancellationToken)
        {
            // 🔹 بررسی مقداردهی ورودی‌ها
            if (request.CartId <= 0 || request.CartItemId <= 0 || request.LanguageId <= 0)
                return ResultModel<bool>.Fail("Invalid parameters.");

            var repo = _unitOfWork.GetRepository<Cart>();

            // 🔹 واکشی سبد خرید با اقلام و ترجمه‌ها
            var cart = await repo.GetAsync(
                predicate: x => x.Id == request.CartId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(c => c.Items)
                               .ThenInclude(i => i.Translations)
            );

            if (cart is null)
                return ResultModel<bool>.Fail("Cart not found.");

            var item = cart.Items.FirstOrDefault(i => i.Id == request.CartItemId);
            if (item is null)
                return ResultModel<bool>.Fail("Cart item not found.");

            try
            {
                // 🔹 حذف ترجمه مورد نظر از آیتم
                item.RemoveTranslation(request.LanguageId);

                // 🔹 ذخیره تغییرات
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<bool>.Success(true);
            }
            catch (DomainException ex)
            {
                return ResultModel<bool>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return ResultModel<bool>.Fail("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
