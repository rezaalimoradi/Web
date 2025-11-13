using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Orders.Carts.Commands
{
    public class AddToCartCommand : IAppRequest<ResultModel>
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// شناسه مشتری (user id یا guest cookie). کنترلر می‌تواند قبل از ارسال مقداردهی کند.
        /// اختیاری است تا در تست‌ها یا فراخوانی‌های قدیمی مشکلی پیش نیاید.
        /// </summary>
        public string? CustomerIdentifier { get; set; }

        public AddToCartCommand() { }

        public AddToCartCommand(long productId, int quantity, string? customerIdentifier = null)
        {
            ProductId = productId;
            Quantity = quantity;
            CustomerIdentifier = customerIdentifier;
        }
    }
}
