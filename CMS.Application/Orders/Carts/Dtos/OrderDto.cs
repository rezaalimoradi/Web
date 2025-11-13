using System;
using System.Collections.Generic;

namespace CMS.Application.Orders.Orders.Dtos
{
    public class OrderDto
    {
        public long Id { get; set; }

        // 🧾 وضعیت سفارش
        public string Status { get; set; } = "در انتظار بررسی";

        // 📅 زمان ثبت سفارش
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        // 👤 اطلاعات مشتری
        public long? CustomerId { get; set; }
        public string? CustomerIdentifier { get; set; }
        public string? CustomerFullName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // 💳 پرداخت و ارسال
        public string PaymentMethod { get; set; } = "نقدی هنگام تحویل";
        public string ShippingMethod { get; set; } = "ارسال رایگان";

        // 💰 جمع مبالغ
        public decimal Subtotal { get; set; } = 0m;                  // جمع قیمت‌ها بدون تخفیف
        public decimal DiscountAmount { get; set; } = 0m;            // مقدار تخفیف
        public decimal TotalPrice { get; set; } = 0m;                // مبلغ نهایی پرداختی
        public string? CouponCode { get; set; }                      // اگر کوپنی استفاده شده باشد

        public decimal? TotalPriceBeforeDiscount { get; set; }

        // 🏠 آدرس‌ها
        public ShippingAddressDto ShippingAddress { get; set; } = new();
        public BillingAddressDto BillingAddress { get; set; } = new();

        // 📦 آیتم‌های سفارش
        public List<OrderItemDto> Items { get; set; } = new();

        // 📝 توضیحات یا یادداشت کاربر
        public string? Notes { get; set; }
    }
}
