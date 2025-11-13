using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Users.Entities;

namespace CMS.Domain.Pricing
{
    public class CartRuleCustomerGroup : BaseEntity
    {
        protected CartRuleCustomerGroup() { } // EF

        public CartRuleCustomerGroup(long cartRuleId, long customerGroupId)
        {
            if (cartRuleId <= 0)
                throw new DomainException("CartRuleId is required.");
            if (customerGroupId <= 0)
                throw new DomainException("CustomerGroupId is required.");

            CartRuleId = cartRuleId;
            CustomerGroupId = customerGroupId;
        }

        public long CartRuleId { get; private set; }
        public CartRule CartRule { get; private set; }

        public long CustomerGroupId { get; private set; }
        public AppUser CustomerGroup { get; private set; }
    }
}
