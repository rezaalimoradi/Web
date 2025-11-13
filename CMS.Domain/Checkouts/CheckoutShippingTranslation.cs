using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Checkouts
{
    public class CheckoutShippingTranslation : BaseEntity
    {
        protected CheckoutShippingTranslation() { } // EF

        public CheckoutShippingTranslation(
            long checkoutShippingId,
            string language,
            string displayName,
            string description)
        {
            if (string.IsNullOrWhiteSpace(language))
                throw new DomainException("Language is required.");
            if (string.IsNullOrWhiteSpace(displayName))
                throw new DomainException("Display name is required.");

            CheckoutShippingId = checkoutShippingId;
            Language = language.Trim().ToLowerInvariant();
            DisplayName = displayName.Trim();
            Description = description?.Trim();
        }

        public long CheckoutShippingId { get; private set; }
        public CheckoutShipping CheckoutShipping { get; private set; }

        /// <summary>
        /// Two-letter language code (e.g. "en", "fa")
        /// </summary>
        public string Language { get; private set; }

        /// <summary>
        /// Localized shipping method name (e.g. "پست پیشتاز", "Express Delivery")
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Localized description or extra info
        /// </summary>
        public string Description { get; private set; }

        // --- Behaviors ---
        public void UpdateTexts(string displayName, string description)
        {
            if (string.IsNullOrWhiteSpace(displayName))
                throw new DomainException("Display name is required.");

            DisplayName = displayName.Trim();
            Description = description?.Trim();
        }
    }
}
