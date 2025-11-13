using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Users.Entities
{
    public class GroupPermission : BaseEntity
    {
        public string Name { get; set; }

        public long WebSiteRoleId { get; set; }

        public WebSiteRole WebSiteRole { get; set; }

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();

        public ICollection<UserGroupPermission> UserGroupPermissions { get; set; } = new List<UserGroupPermission>();
    }
}
