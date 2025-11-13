using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Domain.Users.Entities;

namespace CMS.Application.Users.Queries
{
    public class GetRolePermissionByRoleNameQuery : IAppRequest<List<Permission>>
    {
        public string RoleName { get; set; }

        public long UserId { get; set; }
    }
}
