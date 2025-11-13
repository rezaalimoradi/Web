using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Catalog.Entities
{
    public class ProductRelation : BaseEntity
    {
        protected ProductRelation() { } // EF Core

        public ProductRelation(long productId, long relatedProductId)
        {
            if (productId <= 0 || relatedProductId <= 0)
                throw new DomainException("Invalid ProductId or RelatedProductId.");

            if (productId == relatedProductId)
                throw new DomainException("A product cannot be related to itself.");

            ProductId = productId;
            RelatedProductId = relatedProductId;
        }

        public long ProductId { get; private set; }
        public Product Product { get; private set; }

        public long RelatedProductId { get; private set; }
        public Product RelatedProduct { get; private set; }
    }
}
