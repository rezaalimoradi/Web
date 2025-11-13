//این موجودیت برای لاگ کردن استفاده از کوپن/قانون سبد هست.


using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Users.Entities;

namespace CMS.Domain.Pricing
{
    public class CartRuleUsage : BaseEntity
    {
        protected CartRuleUsage() { } // EF

        public CartRuleUsage(long cartRuleId, long customerId)
        {
            if (cartRuleId <= 0)
                throw new DomainException("CartRuleId is required.");
            if (customerId <= 0)
                throw new DomainException("CustomerId is required.");

            CartRuleId = cartRuleId;
            CustomerId = customerId;
            UsedOn = DateTimeOffset.UtcNow;
        }

        public long CartRuleId { get; private set; }
        public CartRule CartRule { get; private set; }

        public long CustomerId { get; private set; }
        public AppUser Customer { get; private set; }

        public DateTimeOffset UsedOn { get; private set; }
    }
}
