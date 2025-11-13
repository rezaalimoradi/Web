using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Payments
{
    public enum PaymentStatus
    {
        Succeeded = 1,

        Pending = 2,

        Failed = 5
    }
}