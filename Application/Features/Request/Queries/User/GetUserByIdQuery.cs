using Application.Common;
using Shared.Dtos.User;

namespace Application.Features.Request.Queries.User
{
    public class GetUserByIdQuery : IAppRequest<ResultModel<UserDto>>
    {
        public Guid UserId { get; set; }
    }
}
