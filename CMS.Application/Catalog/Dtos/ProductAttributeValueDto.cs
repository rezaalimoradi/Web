using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Dtos
{
    public class ProductAttributeValueDto
    {
        public long Id { get; set; }
        public long AttributeId { get; set; }

        // جدید: آیا این مقدار برای محصولِ جاری انتخاب شده است؟
        public bool IsSelected { get; set; } = false;

        public List<ProductAttributeValueTranslationDto> Translations { get; set; } = new();
    }
}
