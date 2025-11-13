using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User.Enums
{
    public enum PermissionAction
    {
        Read,    // Index, Details, View-only actions
        Create,  // Add new entries
        Edit,    // Update existing entries
        Delete   // Remove entries
    }
}
