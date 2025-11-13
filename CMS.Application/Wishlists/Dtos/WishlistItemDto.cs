using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Wishlists.Dtos
{
    public class WishlistItemDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public decimal Price { get; set; }
        public bool InStock { get; set; }
    }
}
