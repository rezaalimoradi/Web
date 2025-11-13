using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Dtos
{
    public class ProductPriceDto
    {
        public long Id { get; set; }
        public string VariantName { get; set; }  // مثلا "50ml"
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public decimal? SpecialPrice { get; set; }
        public DateTime? SpecialPriceStart { get; set; }
        public DateTime? SpecialPriceEnd { get; set; }
    }
}
