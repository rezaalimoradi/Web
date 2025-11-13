using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Orders.Orders.Dtos;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders;
using CMS.Domain.Orders.Carts;
using CMS.Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.CommandHandlers
{
    public class SubmitOrderCommandHandler : IAppRequestHandler<SubmitOrderCommand, ResultModel<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;
        private readonly UserManager<AppUser> _userManager;

        public SubmitOrderCommandHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext,
            UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ResultModel<OrderDto>> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                return ResultModel<OrderDto>.Fail("درخواست نامعتبر است.");

            var tenantId = _tenantContext.TenantId;
            if (tenantId <= 0)
                return ResultModel<OrderDto>.Fail("Tenant معتبر نیست.");

            // --- 1. شناسه کاربر ---
            var userId = _tenantContext.UserId ?? await EnsureUserExistsAsync(request, cancellationToken);
            if (userId is null)
                return ResultModel<OrderDto>.Fail("کاربر یافت نشد یا امکان ثبت‌نام وجود ندارد.");

            var customerIdentifier = request.CustomerIdentifier
                ?? _tenantContext.CustomerIdentifier
                ?? $"guest-{Guid.NewGuid()}";

            // --- 2. دریافت سبد خرید ---
            var cart = await _unitOfWork.GetRepository<Cart>().Table
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.WebSiteId == tenantId && c.CustomerIdentifier == customerIdentifier, cancellationToken);

            if (cart == null || !cart.Items.Any())
                return ResultModel<OrderDto>.Fail("سبد خرید خالی است.");

            // --- 3. ثبت آدرس ---
            var orderAddress = CreateOrderAddress(request, _tenantContext.CurrentLanguageId);
            await _unitOfWork.GetRepository<OrderAddress>().InsertAsync(orderAddress);
            await _unitOfWork.SaveChangesAsync(cancellationToken); 

            // --- 4. ایجاد سفارش ---
            var order = new Order(userId.Value, userId.Value, orderAddress.Id, orderAddress.Id);
            await _unitOfWork.GetRepository<Order>().InsertAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken); 

            // --- 5. اضافه کردن ترجمه و تاریخچه ---
            order.AddTranslation(
                languageId: _tenantContext.CurrentLanguageId,
                orderNote: request.Notes ?? "",
                shippingMethod: request.ShippingMethod ?? "ارسال رایگان",
                paymentMethod: request.PaymentMethod ?? "نقدی هنگام تحویل",
                updatedById: userId.Value
            );

            order.AddCreatedHistory(userId.Value);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // --- 6. افزودن آیتم‌ها ---
            foreach (var item in cart.Items)
            {
                order.AddOrderItem(item.ProductId, item.Quantity, item.UnitPrice, userId.Value);
            }

            // --- 7. اعمال کوپن ---
            if (!string.IsNullOrWhiteSpace(request.CouponCode))
            {
                order.ApplyCoupon(request.CouponCode, "RuleNamePlaceholder", 0, userId.Value);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // --- 8. حذف سبد خرید و آیتم‌ها ---
            _unitOfWork.GetRepository<Cart>().Delete(cart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // --- 9. ساخت DTO خروجی ---
            var dto = new OrderDto
            {
                Id = order.Id,
                Status = order.OrderStatus.ToString(),
                OrderDate = order.CreatedOn.UtcDateTime,
                TotalPrice = order.OrderTotal,
                TotalPriceBeforeDiscount = order.SubTotal,
                ShippingAddress = new ShippingAddressDto
                {
                    City = request.City,
                    Country = request.Country,
                    Address1 = request.Address1,
                    Address2 = request.Address2 ?? string.Empty,
                    ZipCode = request.ZipCode
                },
                Items = order.OrderItems.Select(i => new OrderItemDto
                {
                    ProductName = i.Product?.Translations?.FirstOrDefault()?.Name ?? "بدون نام محصول",
                    Quantity = i.Quantity,
                    UnitPrice = i.ProductPrice,
                    Discount = i.DiscountAmount
                }).ToList()
            };

            return ResultModel<OrderDto>.Success(dto);
        }

        // ------------------ Helpers ------------------

        private async Task<long?> EnsureUserExistsAsync(SubmitOrderCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return null;

            // بررسی وجود کاربر
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            if (user != null)
                return user.Id;

            // ساخت کاربر جدید
            var newUser = new AppUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.Phone,
                EmailConfirmed = false,
                IsActive = true
            };

            // ایجاد رمز عبور موقت
            var tempPassword = $"Tmp#{Guid.NewGuid():N}".Substring(0, 12);
            var result = await _userManager.CreateAsync(newUser, tempPassword);

            if (!result.Succeeded)
                throw new InvalidOperationException($"خطا در ساخت کاربر جدید: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            // ✅ ذخیره در دیتابیس از طریق UnitOfWork
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newUser.Id;
        }



        private static OrderAddress CreateOrderAddress(SubmitOrderCommand req, long languageId)
        {
            var addr = new OrderAddress(req.Phone, req.ZipCode);
            addr.AddTranslation(
                languageId,
                $"{req.FirstName} {req.LastName}",
                req.Address1,
                req.Address2 ?? "-",
                req.City);
            return addr;
        }
    }
}
