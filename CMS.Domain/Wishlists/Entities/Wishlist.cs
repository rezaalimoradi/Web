using CMS.Domain.Common;
using System;
using System.Collections.Generic;

namespace CMS.Domain.Wishlist.Entities
{
    public class Wishlist : AggregateRoot
    {
        #region Properties
        public long WebsiteId { get; private set; }
        public long CustomerId { get; private set; } // شناسه کاربر
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public ICollection<WishlistItem> Items { get; private set; } = new List<WishlistItem>();

        #endregion

        #region Constructors

        protected Wishlist() { }

        public Wishlist(long websiteId, long customerId)
        {
            WebsiteId = websiteId;
            CustomerId = customerId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        #endregion

        #region Methods

        public void AddProduct(long productId)
        {
            if (Items.Any(i => i.ProductId == productId))
                return;

            Items.Add(new WishlistItem(Id, productId));
            UpdatedAt = DateTime.UtcNow;
        }


        public void RemoveProduct(long productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null) return;

            Items.Remove(item);
            UpdatedAt = DateTime.UtcNow;
        }

        #endregion
    }
}
