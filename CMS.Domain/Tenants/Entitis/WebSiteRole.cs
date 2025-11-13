using CMS.Domain.Common;
using CMS.Domain.Users.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Tenants.Entitis
{
    public class WebSiteRole : BaseEntity
    {
        public long RoleId { get; set; }

        public AppRole Role { get; set; }

        public long WebSiteId { get; set; }

        [ForeignKey(nameof(WebSiteId))]
        public WebSite WebSite { get; set; }

        public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
    }
}
