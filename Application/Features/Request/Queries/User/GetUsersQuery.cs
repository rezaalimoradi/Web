using Application.Common;
using Shared.Dtos.User;

namespace Application.Features.Request.Queries.User
{
    public class GetUsersQuery : IAppRequest<ResultModel<List<UserDto>>>
    {
        public string? SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}