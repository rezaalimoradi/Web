using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Wishlists.Commands
{
    public class DeleteWishlistItemCommand : IAppRequest<ResultModel<bool>>
    {
        public long CustomerIdentifier { get; set; }
        public long ProductId { get; set; }

        public DeleteWishlistItemCommand(long customerIdentifier, long productId)
        {
            CustomerIdentifier = customerIdentifier;
            ProductId = productId;
        }
    }
}
