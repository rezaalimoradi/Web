using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Users.Entities;

namespace CMS.Domain.Pricing
{
    public class CouponUsage : BaseEntity
    {
        protected CouponUsage() { } // EF

        public CouponUsage(long couponId, long customerId)
        {
            if (couponId <= 0)
                throw new DomainException("CouponId is required.");
            if (customerId <= 0)
                throw new DomainException("CustomerId is required.");

            CouponId = couponId;
            CustomerId = customerId;
            UsedOn = DateTimeOffset.UtcNow;
        }

        public long CouponId { get; private set; }
        public Coupon Coupon { get; private set; }

        public long CustomerId { get; private set; }
        public AppUser Customer { get; private set; }

        public DateTimeOffset UsedOn { get; private set; }
    }
}
