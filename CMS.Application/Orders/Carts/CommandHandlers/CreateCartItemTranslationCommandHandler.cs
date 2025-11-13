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
    public class CreateCartItemTranslationCommandHandler
        : IAppRequestHandler<CreateCartItemTranslationCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateCartItemTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateCartItemTranslationCommand request, CancellationToken cancellationToken)
        {
            // اعتبارسنجی ورودی‌ها
            var validationResult = ValidateRequest(request);
            if (!validationResult.IsSuccess)
                return validationResult;

            // بارگذاری Cart به همراه Items و Translationها
            var cart = await GetCartWithItemsAsync(request.CartId, cancellationToken);
            if (cart == null)
                return ResultModel<long>.Fail("Cart not found.");

            var item = cart.Items.SingleOrDefault(i => i.Id == request.CartItemId);
            if (item == null)
                return ResultModel<long>.Fail("Cart item not found.");

            try
            {
                // استفاده از متد Aggregate Root برای اضافه کردن Translation
                item.AddTranslation(request.LanguageId, request.Title);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // بازگرداندن Id Translation اضافه شده
                var translation = item.Translations.First(t => t.WebSiteLanguageId == request.LanguageId);
                return ResultModel<long>.Success(translation.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }

        private ResultModel<long> ValidateRequest(CreateCartItemTranslationCommand request)
        {
            if (request.CartId <= 0)
                return ResultModel<long>.Fail("Invalid CartId.");
            if (request.CartItemId <= 0)
                return ResultModel<long>.Fail("Invalid CartItemId.");
            if (request.LanguageId <= 0)
                return ResultModel<long>.Fail("Invalid LanguageId.");
            if (string.IsNullOrWhiteSpace(request.Title))
                return ResultModel<long>.Fail("Title is required.");
            if (request.Title.Length > 250)
                return ResultModel<long>.Fail("Title cannot exceed 250 characters.");

            return ResultModel<long>.Success(0);
        }

        private async Task<Cart?> GetCartWithItemsAsync(long cartId, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Cart>();
            return await repo.GetAsync(
                predicate: x => x.Id == cartId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(c => c.Items).ThenInclude(i => i.Translations)
            );
        }
    }
}
