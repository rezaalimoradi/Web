using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Dtos
{
    public class ProductAttributeDto
    {
        public long Id { get; set; }
        public bool AllowMultipleValues { get; set; }
        public List<ProductAttributeTranslationDto> Translations { get; set; } = new();
        public List<ProductAttributeValueDto> Values { get; set; } = new();

        public List<string>? SelectedCustomValues { get; set; } = new();

    }
}
