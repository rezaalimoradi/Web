using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Shippings
{
    /// <summary>
    /// AggregateRoot for managing shipping configuration (rules, providers, etc.)
    /// </summary>
    public class Shipping : AggregateRoot
    {
        private readonly List<ShippingFree> _freeRules = new();
        public IReadOnlyCollection<ShippingFree> FreeRules => _freeRules.AsReadOnly();

        private readonly List<ShippingRate> _rates = new();
        public IReadOnlyCollection<ShippingRate> Rates => _rates.AsReadOnly();

        private readonly List<ShippingTranslation> _translations = new();
        public IReadOnlyCollection<ShippingTranslation> Translations => _translations.AsReadOnly();

        protected Shipping() { } // EF

        public Shipping(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                throw new DomainException("System name is required.");

            SystemName = systemName.Trim();
            CreatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedOn = CreatedOn;
            IsEnabled = true;
        }

        public string SystemName { get; private set; }
        public bool IsEnabled { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset LatestUpdatedOn { get; private set; }

        // ---------------- Behaviors ----------------

        public void Enable()
        {
            IsEnabled = true;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void Disable()
        {
            IsEnabled = false;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void UpdateSystemName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("System name is required.");

            SystemName = name.Trim();
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        // ---------------- Free Shipping Rules ----------------

        public void AddFreeRule(decimal minimumOrderAmount)
        {
            var rule = new ShippingFree(minimumOrderAmount);
            _freeRules.Add(rule);
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void RemoveFreeRule(long ruleId)
        {
            var rule = _freeRules.FirstOrDefault(r => r.Id == ruleId);
            if (rule == null)
                throw new DomainException("Free shipping rule not found.");

            _freeRules.Remove(rule);
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        // ---------------- Rate Rules ----------------

        public void AddRate(decimal rate, decimal? minimumOrderAmount = null)
        {
            var shippingRate = new ShippingRate(rate);
            if (minimumOrderAmount.HasValue)
                shippingRate.Update(rate, minimumOrderAmount);

            _rates.Add(shippingRate);
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void RemoveRate(long rateId)
        {
            var rate = _rates.FirstOrDefault(r => r.Id == rateId);
            if (rate == null)
                throw new DomainException("Rate not found.");

            _rates.Remove(rate);
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        // ---------------- Translations ----------------

        public void AddTranslation(long languageId, string name, string description = null)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new ShippingTranslation(Id, languageId, name, description));
        }

        public void UpdateTranslation(long languageId, string name, string description = null)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(name, description);
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }

        // ---------------- Business Logic ----------------

        /// <summary>
        /// Calculate applicable shipping fee for an order total.
        /// Returns 0 if free rule applies, otherwise lowest matching rate.
        /// </summary>
        public decimal CalculateFee(decimal orderTotal)
        {
            // اول بررسی قوانین حمل‌ونقل رایگان
            if (_freeRules.Any(rule => rule.IsSatisfiedBy(orderTotal)))
                return 0;

            // اگر ریتی تعریف شده، کمترین نرخ مناسب برمی‌گردد
            var applicableRates = _rates.Where(r => r.IsSatisfiedBy(orderTotal)).ToList();
            if (applicableRates.Any())
                return applicableRates.Min(r => r.Rate);

            throw new DomainException("No applicable shipping rule found.");
        }
    }
}
