using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.CompareItems.Dtos
{
    public class CompareItemsDto
    {
        public long ProductId { get; set; }
        public long CustomerId { get; set; }
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = "";
        public bool InStock { get; set; }
        public string Brand { get; set; } = "";
        public string ScentType { get; set; } = "";
        public int ProductionYear { get; set; }
        public DateTime AddedAt { get; set; }
        public List<string> ImageUrls { get; set; } = new();
    }
}
