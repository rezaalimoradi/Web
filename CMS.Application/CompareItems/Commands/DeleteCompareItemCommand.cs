using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.CompareItems.Commands
{
    public class DeleteCompareItemCommand : IAppRequest<ResultModel<bool>>
    {
        public long CustomerId { get; set; }
        public long ProductId { get; set; }
        public long WebsiteId { get; set; }

        public DeleteCompareItemCommand(long customerId, long websiteId, long productId)
        {
            CustomerId = customerId;
            WebsiteId = websiteId;
            ProductId = productId;
        }
    }
}
