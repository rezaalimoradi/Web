using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Commands
{
    public class DeleteProductRelationCommand : IAppRequest<ResultModel<long>>
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long RelatedProductId { get; set; }
    }
}
