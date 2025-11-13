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
    public class CreateCartTranslationCommandHandler
        : IAppRequestHandler<CreateCartTranslationCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateCartTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateCartTranslationCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ اعتبارسنجی ورودی‌ها
            var validationResult = ValidateRequest(request);
            if (!validationResult.IsSuccess)
                return validationResult;

            // 2️⃣ بارگذاری Cart با Translationها
            var cart = await GetCartAsync(request.CartId, cancellationToken);
            if (cart == null)
                return ResultModel<long>.Fail("Cart not found.");

            try
            {
                // 3️⃣ اضافه کردن Translation از طریق Aggregate Root
                cart.AddTranslation(request.LanguageId, request.Title, request.Description);

                // 4️⃣ ذخیره تغییرات
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // 5️⃣ بازگرداندن Id Translation اضافه شده
                var translation = cart.Translations.First(t => t.WebSiteLanguageId == request.LanguageId);
                return ResultModel<long>.Success(translation.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }

        // ========================
        // اعتبارسنجی ورودی‌ها
        // ========================
        private ResultModel<long> ValidateRequest(CreateCartTranslationCommand request)
        {
            if (request.LanguageId <= 0)
                return ResultModel<long>.Fail("Invalid LanguageId.");
            if (string.IsNullOrWhiteSpace(request.Title))
                return ResultModel<long>.Fail("Title is required.");
            if (request.Title.Length > 250)
                return ResultModel<long>.Fail("Title cannot exceed 250 characters.");
            if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 1000)
                return ResultModel<long>.Fail("Description cannot exceed 1000 characters.");

            return ResultModel<long>.Success(0);
        }

        // ========================
        // متد کمکی برای بارگذاری Cart
        // ========================
        private async Task<Cart?> GetCartAsync(long cartId, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Cart>();
            return await repo.GetAsync(
                predicate: x => x.Id == cartId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(c => c.Translations)
            );
        }
    }
}
