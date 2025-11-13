using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Wishlists.Dtos
{
    public class WishlistDto
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public List<WishlistItemDto> Items { get; set; } = new();
    }
}
