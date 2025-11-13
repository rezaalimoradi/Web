using CMS.Application.Orders.Carts.QueryHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.Dtos
{
    public class CartItemDto
    {
        public long Id { get; set; }

        public long CartId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public List<CartItemTranslationDto> Translations { get; set; } = new();
        public string ProductName { get; set; }
    }
}
