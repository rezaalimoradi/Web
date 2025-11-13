using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Commands
{
    public class CreateProductPriceCommand : IAppRequest<ResultModel<long>>
    {
        public long ProductId { get; set; }
        public string VariantName { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public int StockQuantity { get; set; }
        public decimal? SpecialPrice { get; set; }
        public DateTime? SpecialPriceStart { get; set; }
        public DateTime? SpecialPriceEnd { get; set; }
    }
}
