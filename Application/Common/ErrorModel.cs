using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class ErrorModel
    {
        public string Message { get; set; }

        public string Issuer { get; set; }

        public int Code { get; set; }
    }
}
