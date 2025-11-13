using CMS.Domain.Common;
using Domain.User.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User.Entities
{
    public partial class Permission : BaseEntity
    {
        public string Area { get; set; }

        public string Controller { get; set; }

        public PermissionAction Action { get; set; }
    }
}
