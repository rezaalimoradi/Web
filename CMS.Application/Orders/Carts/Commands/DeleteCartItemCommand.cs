using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Orders.Carts.Dtos;

namespace CMS.Application.Orders.Carts.Commands
{
    public class DeleteCartItemCommand : IAppRequest<ResultModel<CartUpdateResultDto>>
    {
        public long CartId { get; set; }
        public string CustomerIdentifier { get; set; } = string.Empty;
        public long ProductId { get; set; }

        public DeleteCartItemCommand() { }

        public DeleteCartItemCommand(string customerIdentifier, long productId)
        {
            CustomerIdentifier = customerIdentifier;
            ProductId = productId;
        }
    }
}
