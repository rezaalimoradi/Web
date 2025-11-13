using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;

namespace CMS.Application.Orders.Carts.Commands
{
    public class UpdateCartItemCommand : IAppRequest<ResultModel<CartUpdateResultDto>>
    {
        public long CartId { get; set; }
        public string CustomerIdentifier { get; set; } = string.Empty;
        public long ProductId { get; set; }
        public int Quantity { get; set; }

        public UpdateCartItemCommand() { }

        public UpdateCartItemCommand(string customerIdentifier, long productId, int quantity)
        {
            CustomerIdentifier = customerIdentifier;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
