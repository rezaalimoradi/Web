using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Orders.Carts.Dtos;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.CommandHandlers
{
    public class AddToCartCommandHandler : IAppRequestHandler<AddToCartCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public AddToCartCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
        }

        public async Task<ResultModel> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                return ResultModel.Fail("درخواست نامعتبر است.");

            if (request.Quantity <= 0)
                return ResultModel.Fail("تعداد باید بیشتر از صفر باشد.");

            var tenantId = _tenantContext.TenantId;
            if (tenantId <= 0)
                return ResultModel.Fail("Tenant معتبر نیست.");

            // 🟩 تعیین CustomerIdentifier نهایی
            var userIdentifier = _tenantContext.UserId?.ToString();
            var guestIdentifier = _tenantContext.CustomerIdentifier ?? $"guest-{Guid.NewGuid()}";
            var currentIdentifier = request.CustomerIdentifier ?? userIdentifier ?? guestIdentifier;

            var cartRepo = _unitOfWork.GetRepository<Cart>();
            var productRepo = _unitOfWork.GetRepository<Product>();

            // 🟢 پیدا کردن سبد فعلی کاربر یا مهمان
            var cart = await cartRepo.Table
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c =>
                    c.WebSiteId == tenantId &&
                    c.CustomerIdentifier == currentIdentifier,
                    cancellationToken);

            // 🟣 اگر کاربر لاگین کرده ولی سبد ندارد، سبد guest را merge کن
            if (cart == null && userIdentifier != null)
            {
                var guestCart = await cartRepo.Table
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c =>
                        c.WebSiteId == tenantId &&
                        c.CustomerIdentifier == guestIdentifier,
                        cancellationToken);

                if (guestCart != null)
                {
                    guestCart.MergeWithUser(userIdentifier);
                    cart = guestCart;
                }
            }

            // 🟠 اگر هنوز سبدی وجود ندارد، بساز
            if (cart == null)
            {
                cart = new Cart(tenantId, currentIdentifier);
                await cartRepo.InsertAsync(cart);
                await _unitOfWork.SaveChangesAsync(cancellationToken); // برای تولید Id
            }

            // 🔵 بررسی موجود بودن محصول
            var product = await productRepo.Table
                .Include(p => p.Translations)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
                return ResultModel.Fail("محصول مورد نظر یافت نشد.");

            var unitPrice = product.Price;

            // 🧩 اضافه کردن آیتم
            try
            {
                cart.AddItem(request.ProductId, request.Quantity, unitPrice);
            }
            catch (DomainException ex)
            {
                return ResultModel.Fail(ex.Message);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // ✅ برگرداندن مدل نهایی سبد
            var cartDto = new CartDto
            {
                Id = cart.Id,
                CustomerIdentifier = cart.CustomerIdentifier,
                Items = cart.Items.Select(i => new CartItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    ProductName = i.Product?.Translations?.FirstOrDefault()?.Name
                                  ?? product.Translations.FirstOrDefault()?.Name
                                  ?? "بدون نام محصول",
                }).ToList()
            };

            cartDto.TotalPrice = cartDto.Items.Sum(i => (i.UnitPrice - i.Discount) * i.Quantity);

            return ResultModel.Success(cartDto);
        }
    }
}
