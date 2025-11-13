using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;

namespace CMS.Domain.Wishlist.Entities
{
    public class WishlistItem : BaseEntity
    {
        public long WishlistId { get; private set; }
        public long ProductId { get; private set; }
        public DateTime AddedAt { get; private set; }

        public Wishlist Wishlist { get; private set; }
        public Product Product { get; private set; }

        protected WishlistItem() { }

        public WishlistItem(long wishlistId, long productId)
        {
            WishlistId = wishlistId;
            ProductId = productId;
            AddedAt = DateTime.UtcNow;
        }
    }
}
