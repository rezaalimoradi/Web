using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Pricing
{
    public class CartRule : AggregateRoot
    {
        private readonly List<CartRuleTranslation> _translations = new();
        public IReadOnlyCollection<CartRuleTranslation> Translations => _translations.AsReadOnly();

        protected CartRule() { } // EF

        public CartRule(
            string name,
            decimal discountAmount,
            bool isActive,
            DateTimeOffset? startOn,
            DateTimeOffset? endOn,
            string description = null,
            string ruleToApply = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("CartRule name is required.");
            if (discountAmount < 0)
                throw new DomainException("DiscountAmount must be non-negative.");
            if (startOn.HasValue && endOn.HasValue && endOn < startOn)
                throw new DomainException("End date cannot be earlier than start date.");

            Name = name.Trim();
            Description = description?.Trim();
            RuleToApply = ruleToApply?.Trim();
            DiscountAmount = discountAmount;
            IsActive = isActive;
            StartOn = startOn;
            EndOn = endOn;

            CreatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedOn = CreatedOn;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public bool IsActive { get; private set; }
        public bool IsCouponRequired { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset LatestUpdatedOn { get; private set; }

        public DateTimeOffset? StartOn { get; private set; }
        public DateTimeOffset? EndOn { get; private set; }

        public string RuleToApply { get; private set; }

        public decimal DiscountAmount { get; private set; }
        public decimal? MaxDiscountAmount { get; private set; }
        public int? DiscountStep { get; private set; }
        public int? UsageLimitPerCoupon { get; private set; }
        public int? UsageLimitPerCustomer { get; private set; }

        // --- Behaviors ---

        public void Update(
            string name,
            string description,
            decimal discountAmount,
            decimal? maxDiscountAmount,
            int? discountStep,
            bool isCouponRequired,
            string ruleToApply,
            DateTimeOffset? startOn,
            DateTimeOffset? endOn)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("CartRule name is required.");
            if (discountAmount < 0)
                throw new DomainException("DiscountAmount must be non-negative.");
            if (maxDiscountAmount.HasValue && maxDiscountAmount < 0)
                throw new DomainException("MaxDiscountAmount must be non-negative.");
            if (startOn.HasValue && endOn.HasValue && endOn < startOn)
                throw new DomainException("End date cannot be earlier than start date.");

            Name = name.Trim();
            Description = description?.Trim();
            DiscountAmount = discountAmount;
            MaxDiscountAmount = maxDiscountAmount;
            DiscountStep = discountStep;
            IsCouponRequired = isCouponRequired;
            RuleToApply = ruleToApply?.Trim();
            StartOn = startOn;
            EndOn = endOn;

            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        // --- Translation behaviors ---

        public void AddTranslation(string culture, string name, string description = null)
        {
            culture = culture?.Trim().ToLowerInvariant();
            if (_translations.Any(t => t.Culture == culture))
                throw new DomainException($"Translation for culture '{culture}' already exists.");

            _translations.Add(new CartRuleTranslation(Id, culture, name, description));
        }

        public void UpdateTranslation(string culture, string name, string description = null)
        {
            culture = culture?.Trim().ToLowerInvariant();
            var translation = _translations.FirstOrDefault(t => t.Culture == culture);
            if (translation == null)
                throw new DomainException($"Translation for culture '{culture}' not found.");

            translation.Update(name, description);
        }

        public void RemoveTranslation(string culture)
        {
            culture = culture?.Trim().ToLowerInvariant();
            var translation = _translations.FirstOrDefault(t => t.Culture == culture);
            if (translation == null)
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }

        public CartRuleTranslation GetTranslation(string culture)
        {
            culture = culture?.Trim().ToLowerInvariant();
            return _translations.FirstOrDefault(t => t.Culture == culture);
        }
    }
}
