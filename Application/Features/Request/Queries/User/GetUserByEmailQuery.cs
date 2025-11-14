// Application/Features/Request/Queries/User/GetUserByEmailQuery.cs
using Application.Common;
using Shared.Dtos.User;

namespace Application.Features.Request.Queries.User
{
    public class GetUserByEmailQuery : IAppRequest<ResultModel<UserDto>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
