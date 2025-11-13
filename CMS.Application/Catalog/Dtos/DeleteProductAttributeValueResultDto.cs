using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Dtos
{
    public class DeleteProductAttributeValueResultDto
    {
        public long RemovedValueId { get; set; }
        public List<long> RemainingValueIds { get; set; } = new();
    }
}
