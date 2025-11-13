using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using System;

namespace CMS.Domain.CompareItems.Entities
{
    public class CompareItem : BaseEntity
    {
        public long ProductId { get; private set; }
        public DateTime AddedAt { get; private set; }

        public long CompareListId { get; private set; }
        public CompareList CompareList { get; private set; }

        public Product Product { get; private set; }

        protected CompareItem() { }

        public CompareItem(long compareListId, long productId)
        {
            CompareListId = compareListId;
            ProductId = productId;
            AddedAt = DateTime.UtcNow;
        }

    }
}
