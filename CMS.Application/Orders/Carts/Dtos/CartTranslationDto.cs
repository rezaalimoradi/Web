using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.Dtos
{
    public class CartTranslationDto
    {
        public long Id { get; set; }

        public long CartId { get; set; }
        public long LanguageId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string LanguageName { get; set; }
    }
}
