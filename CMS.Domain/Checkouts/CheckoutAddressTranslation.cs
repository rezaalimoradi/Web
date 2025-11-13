using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Checkouts
{
    public class CheckoutAddressTranslation : BaseEntity
    {
        protected CheckoutAddressTranslation() { } // EF

        public CheckoutAddressTranslation(long checkoutAddressId, long languageId, string city, string addressLine1, string addressLine2)
        {
            if (checkoutAddressId <= 0)
                throw new DomainException("CheckoutAddressId is required.");
            if (languageId <= 0)
                throw new DomainException("LanguageId is required.");

            CheckoutAddressId = checkoutAddressId;
            WebSiteLanguageId = languageId;
            Update(city, addressLine1, addressLine2);
        }

        public long CheckoutAddressId { get; private set; }
        public CheckoutAddress CheckoutAddress { get; private set; }

        public long WebSiteLanguageId { get; private set; }

        public string City { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }

        // --- Behaviors ---
        public void Update(string city, string addressLine1, string addressLine2)
        {
            City = city?.Trim();
            AddressLine1 = addressLine1?.Trim();
            AddressLine2 = addressLine2?.Trim();
        }
    }
}
