using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Shippings
{
    /// <summary>
    /// Represents shipping rate rule (e.g. flat rate, weight-based, location-based).
    /// </summary>
    public class ShippingRate : AggregateRoot
    {
        private readonly List<ShippingRateTranslation> _translations = new();
        public IReadOnlyCollection<ShippingRateTranslation> Translations => _translations.AsReadOnly();

        protected ShippingRate() { } // EF

        public ShippingRate(decimal rate, bool isEnabled = true)
        {
            if (rate < 0)
                throw new DomainException("Rate cannot be negative.");

            Rate = rate;
            IsEnabled = isEnabled;
            CreatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedOn = CreatedOn;
        }

        /// <summary>
        /// Base rate for shipping (flat amount).
        /// </summary>
        public decimal Rate { get; private set; }

        /// <summary>
        /// Optional minimum order total for this rate to apply.
        /// </summary>
        public decimal? MinimumOrderAmount { get; private set; }

        public bool IsEnabled { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset LatestUpdatedOn { get; private set; }

        // ---------------- Behaviors ----------------

        public void Update(decimal rate, decimal? minimumOrderAmount = null)
        {
            if (rate < 0)
                throw new DomainException("Rate cannot be negative.");
            if (minimumOrderAmount < 0)
                throw new DomainException("Minimum order amount cannot be negative.");

            Rate = rate;
            MinimumOrderAmount = minimumOrderAmount;
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

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

        public bool IsSatisfiedBy(decimal orderTotal) =>
            IsEnabled && (MinimumOrderAmount == null || orderTotal >= MinimumOrderAmount);

        // ---------------- Translations ----------------

        public void AddTranslation(long languageId, string name, string description = null)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new ShippingRateTranslation(Id, languageId, name, description));
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
    }
}
