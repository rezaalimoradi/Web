using Application.Common;
using Shared.Dtos.User;

namespace Application.Features.Request.Queries.User
{
    public class GetUserByEmailQuery : IAppRequest<ResultModel<List<UserDto>>>
    {
        public string Email { get; set; } = string.Empty;
    }
}