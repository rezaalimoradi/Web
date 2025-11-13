using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Pricing
{
    public class CartRuleProduct : BaseEntity
    {
        protected CartRuleProduct() { } // EF

        public CartRuleProduct(long cartRuleId, long productId)
        {
            if (cartRuleId <= 0)
                throw new DomainException("CartRuleId is required.");
            if (productId <= 0)
                throw new DomainException("ProductId is required.");

            CartRuleId = cartRuleId;
            ProductId = productId;
        }

        public long CartRuleId { get; private set; }
        public CartRule CartRule { get; private set; }

        public long ProductId { get; private set; }
        public Product Product { get; private set; }
    }
}
