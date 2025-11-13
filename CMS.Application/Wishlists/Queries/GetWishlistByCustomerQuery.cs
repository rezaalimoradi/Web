using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Wishlists.Dtos;

namespace CMS.Application.Wishlists.Queries
{
    public class GetWishlistByCustomerQuery : IAppRequest<ResultModel<WishlistDto>>
    {
        public long CustomerIdentifier { get; set; }

        public GetWishlistByCustomerQuery(long customerIdentifier)
        {
            CustomerIdentifier = customerIdentifier;
        }
    }
}
