using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Orders.Dtos;

namespace CMS.Application.Orders.Carts.Commands
{
    public class SubmitOrderCommand : IAppRequest<ResultModel<OrderDto>>
    {
        /// <summary>
        /// شناسه مشتری فعلی (ممکن است کاربر لاگین‌شده یا مهمان باشد)
        /// </summary>
        public string? CustomerIdentifier { get; set; }

        // 🧾 اطلاعات مشتری
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string City { get; set; } = string.Empty;
        public string? State { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // 📝 توضیحات سفارش
        public string? Notes { get; set; }

        // 🚚 اطلاعات پرداخت و ارسال
        public string? ShippingMethod { get; set; }
        public string? PaymentMethod { get; set; }

        // 🎟️ کوپن تخفیف
        public string? CouponCode { get; set; }

        // 👤 آیا کاربر مهمان به صورت خودکار ثبت‌نام شود؟
        public bool AutoRegisterGuest { get; set; } = true;
    }
}
