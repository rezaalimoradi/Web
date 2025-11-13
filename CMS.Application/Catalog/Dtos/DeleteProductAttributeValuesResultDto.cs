using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Dtos
{
    public class DeleteProductAttributeValuesResultDto
    {
        public List<long> AddedIds { get; set; } = new();
        public List<long> RemovedIds { get; set; } = new();
        public List<long> AlreadyExistsIds { get; set; } = new();
    }
}
