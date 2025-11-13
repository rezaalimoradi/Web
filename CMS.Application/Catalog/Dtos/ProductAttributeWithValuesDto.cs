using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Dtos
{
    public class ProductAttributeWithValuesDto
    {
        public long AttributeId { get; set; }
        public string AttributeName { get; set; }
        public bool AllowMultipleValues { get; set; }
        public List<ProductAttributeValueDto> Values { get; set; } = new();
    }

}
