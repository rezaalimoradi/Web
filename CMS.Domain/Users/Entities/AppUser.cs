using CMS.Domain.Tenants.Entitis;
using Microsoft.AspNetCore.Identity;

namespace CMS.Domain.Users.Entities
{
    public partial class AppUser : IdentityUser<long>
    {
        public long WebSiteId { get; set; }

        public bool IsActive { get; set; }

        public ICollection<UserGroupPermission> UserGroupPermissions { get; set; } = new List<UserGroupPermission>();

        public virtual WebSite WebSite { get; set; }
    }
}
