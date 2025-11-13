using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Localization.Entities;

namespace CMS.Domain.Shippings
{
    public class ShippingMethod : BaseEntity
    {
        private readonly List<ShippingMethodTranslation> _translations = new();
        public IReadOnlyCollection<ShippingMethodTranslation> Translations => _translations.AsReadOnly();

        protected ShippingMethod() { } // EF

        public ShippingMethod(long providerId, decimal baseFee, int estimatedDeliveryDays)
        {
            if (baseFee < 0)
                throw new DomainException("Base fee cannot be negative.");
            if (estimatedDeliveryDays < 0)
                throw new DomainException("Estimated delivery days must be non-negative.");

            ShippingProviderId = providerId;
            BaseFee = baseFee;
            EstimatedDeliveryDays = estimatedDeliveryDays;
            IsEnabled = true;
        }

        public long ShippingProviderId { get; private set; }
        public ShippingProvider ShippingProvider { get; private set; }

        public decimal BaseFee { get; private set; }
        public int EstimatedDeliveryDays { get; private set; }
        public bool IsEnabled { get; private set; }

        // ---------- Behaviors ----------

        public void Update(decimal baseFee, int estimatedDeliveryDays)
        {
            if (baseFee < 0)
                throw new DomainException("Base fee cannot be negative.");
            if (estimatedDeliveryDays < 0)
                throw new DomainException("Estimated delivery days must be non-negative.");

            BaseFee = baseFee;
            EstimatedDeliveryDays = estimatedDeliveryDays;
        }

        public void Enable() => IsEnabled = true;
        public void Disable() => IsEnabled = false;

        // ---------- Translations ----------

        public void AddTranslation(long languageId, string name, string description)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new ShippingMethodTranslation(Id, languageId, name, description));
        }

        public void UpdateTranslation(long languageId, string name, string description)
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
