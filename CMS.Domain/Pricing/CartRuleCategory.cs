using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Pricing
{
    public class CartRuleCategory : BaseEntity
    {
        protected CartRuleCategory() { } // EF

        public CartRuleCategory(long cartRuleId, long categoryId)
        {
            if (cartRuleId <= 0)
                throw new DomainException("CartRuleId is required.");
            if (categoryId <= 0)
                throw new DomainException("CategoryId is required.");

            CartRuleId = cartRuleId;
            CategoryId = categoryId;
        }

        public long CartRuleId { get; private set; }
        public CartRule CartRule { get; private set; }

        public long CategoryId { get; private set; }
        public ProductCategory Category { get; private set; }
    }
}
