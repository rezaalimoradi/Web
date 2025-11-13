using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Pricing
{
    public class CouponTranslation : BaseEntity
    {
        protected CouponTranslation() { } // EF

        public CouponTranslation(long couponId, string culture, string name, string description = null)
        {
            if (couponId <= 0)
                throw new DomainException("CouponId is required.");
            if (string.IsNullOrWhiteSpace(culture))
                throw new DomainException("Culture is required.");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required.");

            CouponId = couponId;
            Culture = culture;
            Name = name;
            Description = description;
        }

        public long CouponId { get; private set; }
        public Coupon Coupon { get; private set; }

        /// <summary>
        /// Culture code, e.g. "en-US", "fa-IR"
        /// </summary>
        public string Culture { get; private set; }

        /// <summary>
        /// Localized display name for the coupon
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Optional localized description
        /// </summary>
        public string Description { get; private set; }
    }
}
