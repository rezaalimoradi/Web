using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Commands
{
    public class CreateProductRelationCommand : IAppRequest<ResultModel<long>>
    {
        public long ProductId { get; set; }
        public List<long> RelatedProductIds { get; set; } = new();

    }
}
