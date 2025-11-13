//کوپن معمولاً یه کد تخفیف هست که به یک CartRule وصل میشه و مشتری موقع Checkout استفاده می‌کنه.

using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Pricing
{
    public class Coupon : BaseEntity
    {
        protected Coupon() { } // EF

        private readonly List<CouponTranslation> _translations = new();
        public IReadOnlyCollection<CouponTranslation> Translations => _translations.AsReadOnly();

        public Coupon(long cartRuleId, string code, DateTimeOffset? expiresOn = null)
        {
            if (cartRuleId <= 0)
                throw new DomainException("CartRuleId is required.");
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Coupon code is required.");

            CartRuleId = cartRuleId;
            Code = code.Trim().ToUpperInvariant();
            ExpiresOn = expiresOn;
            IsActive = true;
        }

        [Required]
        [StringLength(100)]
        public string Code { get; private set; }

        public long CartRuleId { get; private set; }
        public CartRule CartRule { get; private set; }

        public bool IsActive { get; private set; }

        public DateTimeOffset? ExpiresOn { get; private set; }

        private readonly List<CouponUsage> _usages = new();
        public IReadOnlyCollection<CouponUsage> Usages => _usages.AsReadOnly();

        // --- Behaviors ---

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;

        public bool IsExpired() =>
            ExpiresOn.HasValue && ExpiresOn.Value < DateTimeOffset.UtcNow;

        public bool CanBeUsed() =>
            IsActive && !IsExpired();

        public void RegisterUsage(long customerId)
        {
            if (!CanBeUsed())
                throw new DomainException("Coupon cannot be used.");

            _usages.Add(new CouponUsage(Id, customerId));
        }


        public void AddTranslation(string culture, string name, string description = null)
        {
            if (_translations.Any(t => t.Culture == culture))
                throw new DomainException($"Translation for culture '{culture}' already exists.");

            _translations.Add(new CouponTranslation(Id, culture, name, description));
        }

        public void UpdateTranslation(string culture, string name, string description = null)
        {
            var translation = _translations.FirstOrDefault(t => t.Culture == culture);
            if (translation == null)
                throw new DomainException($"Translation for culture '{culture}' not found.");

            _translations.Remove(translation);
            _translations.Add(new CouponTranslation(Id, culture, name, description));
        }
    }
}
