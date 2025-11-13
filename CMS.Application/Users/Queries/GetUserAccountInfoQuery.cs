using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Orders.Carts.Dtos;

namespace CMS.Application.Users.Queries
{
    public class GetUserAccountInfoQuery : IAppRequest<AccountViewModel>
    {
        public long UserId { get; set; }

        public GetUserAccountInfoQuery(long userId)
        {
            UserId = userId;
        }
    }
}
