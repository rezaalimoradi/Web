using Application.Common;
using Domain.User.Entities;

namespace CMS.Application.Users.Queries
{
    public class GetRolePermissionByRoleNameQuery : IAppRequest<List<Permission>>
    {
        public string RoleName { get; set; }

        public long UserId { get; set; }
    }
}
