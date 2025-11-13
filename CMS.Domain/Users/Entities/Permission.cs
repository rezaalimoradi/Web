using CMS.Domain.Common;
using CMS.Domain.Tenants.Enums;

namespace CMS.Domain.Users.Entities
{
    public partial class Permission : BaseEntity
    {
        public string Area { get; set; }

        public string Controller { get; set; }

        public PermissionAction Action { get; set; }
    }
}
