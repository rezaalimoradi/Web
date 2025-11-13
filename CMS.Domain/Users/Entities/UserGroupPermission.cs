using CMS.Domain.Common;
using CMS.Domain.Users.Entities;

namespace CMS.Domain.Users.Entities
{
    public class UserGroupPermission : BaseEntity
    {
        public long UserId { get; set; }

        public long GroupPermissionId { get; set; }

        public AppUser User { get; set; }

        public GroupPermission GroupPermission { get; set; }
    }
}
