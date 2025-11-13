using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Orders.Carts
{
    public enum CartStatus
    {
        Active = 0,
        CheckedOut = 1,
        Abandoned = 2
    }
}
