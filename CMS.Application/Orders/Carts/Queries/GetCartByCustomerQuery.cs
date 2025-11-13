using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;

namespace CMS.Application.Orders.Carts.Queries
{
    public class GetCartByCustomerQuery : IAppRequest<ResultModel<CartDto>>
    {
        /// <summary>
        /// شناسه مشتری (کاربر لاگین شده یا مهمان)
        /// </summary>
        public string CustomerIdentifier { get; set; }

        /// <summary>
        /// کانستراکتور با مقدار اجباری
        /// </summary>
        /// <param name="customerIdentifier"></param>
        public GetCartByCustomerQuery(string customerIdentifier)
        {
            CustomerIdentifier = customerIdentifier ?? throw new ArgumentNullException(nameof(customerIdentifier));
        }

        /// <summary>
        /// کانستراکتور پیش‌فرض (اختیاری برای object initializer)
        /// </summary>
        public GetCartByCustomerQuery()
        {
            CustomerIdentifier = string.Empty;
        }
    }
}
