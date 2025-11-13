using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Orders.Dtos;
using CMS.Domain.Common;
using CMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Orders.QueryHandlers
{
    public class GetOrderByIdQueryHandler : IAppRequestHandler<GetOrderByIdQuery, ResultModel<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ResultModel<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                return ResultModel<OrderDto>.Fail("درخواست نامعتبر است.");

            // بارگذاری سفارش به همراه آیتم‌ها، محصولات، ترجمه‌ها، آدرس و تاریخچه
            var order = await _unitOfWork.GetRepository<Order>().Table
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Translations)
                .Include(o => o.Translations)
                .Include(o => o.ShippingAddress).ThenInclude(a => a.Translations)
                .Include(o => o.BillingAddress).ThenInclude(a => a.Translations)
                .Include(o => o.Histories)
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (order == null)
                return ResultModel<OrderDto>.Fail("سفارش پیدا نشد.");

            // اعتبارسنجی مالک سفارش:
            // اگر CustomerIdentifier ارسال شده و قابل تبدیل به long است آن را با CustomerId مقایسه می‌کنیم.
            // در صورت عدم تطابق اجازه نمایش داده نمی‌شود.
            if (!string.IsNullOrWhiteSpace(request.CustomerIdentifier))
            {
                if (long.TryParse(request.CustomerIdentifier, out var parsedCustomerId))
                {
                    if (order.CustomerId != parsedCustomerId)
                        return ResultModel<OrderDto>.Fail("شما مجاز به مشاهده این سفارش نیستید.");
                }
                else
                {
                    // اگر CustomerIdentifier قالب دیگری (مثلاً guest-...) است، می‌توانید منطق دیگری اعمال کنید.
                    // فعلاً از دسترسی جلوگیری می‌کنیم.
                    return ResultModel<OrderDto>.Fail("شناسه مشتری نامعتبر است.");
                }
            }

            // استخراج ترجمهٔ مناسب از آدرس/محصول (اولین ترجمه موجود)
            var shipTrans = order.ShippingAddress?.Translations?.FirstOrDefault();
            var billTrans = order.BillingAddress?.Translations?.FirstOrDefault();

            // ساخت DTO خروجی با اطلاعات بیشتر
            var dto = new OrderDto
            {
                Id = order.Id,
                Status = order.OrderStatus.ToString(),
                OrderDate = order.CreatedOn.UtcDateTime,
                TotalPrice = order.OrderTotal,
                TotalPriceBeforeDiscount = order.SubTotal,
                ShippingAddress = new ShippingAddressDto
                {
                    Address1 = shipTrans?.AddressLine1 ?? string.Empty,
                    Address2 = shipTrans?.AddressLine2 ?? string.Empty,
                    City = shipTrans?.City ?? string.Empty,
                    ZipCode = order.ShippingAddress?.ZipCode ?? string.Empty,
                    Country = "" // اگر فیلد Country در آدرس دارید آن را اینجا پر کنید
                },
                Items = order.OrderItems.Select(i => new OrderItemDto
                {
                    ProductName = i.Product?.Translations?.FirstOrDefault()?.Name ?? "بدون نام محصول",
                    Quantity = i.Quantity,
                    UnitPrice = i.ProductPrice,
                    Discount = i.DiscountAmount
                }).ToList()
            };

            // تاریخچه رو هم اضافه می‌کنیم (اگر توی DTO خواستید)
            // dto.Histories = order.Histories.Select(h => new OrderHistoryDto { ... }).ToList();

            return ResultModel<OrderDto>.Success(dto);
        }
    }
}
