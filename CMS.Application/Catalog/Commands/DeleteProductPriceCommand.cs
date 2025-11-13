using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Commands
{
    public class DeleteProductPriceCommand : IAppRequest<ResultModel>
    {
        public long ProductId { get; set; }
        public long PriceId { get; set; }
    }
}
