namespace CMS.Application.Orders.Orders.Dtos
{
    public class OrderItemDto
    {
        public long Id { get; set; }

        // 📦 محصول
        public long ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductSku { get; set; }
        public string? ProductImage { get; set; }

        // 🔢 تعداد و قیمت
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; } = 0m;        // قیمت واحد
        public decimal Discount { get; set; } = 0m;         // تخفیف روی آیتم
        public decimal TaxAmount { get; set; } = 0m;        // مالیات
        public decimal TaxPercent { get; set; } = 0m;

        // 💰 قیمت نهایی آیتم
        public decimal Total => (UnitPrice * Quantity) - Discount + TaxAmount;

        // 📝 توضیحات محصول (ترجمه یا توضیح سفارشی)
        public string? Description { get; set; }
    }
}
