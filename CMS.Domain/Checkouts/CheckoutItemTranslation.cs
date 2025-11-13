using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Checkouts
{
    public class CheckoutItemTranslation : BaseEntity
    {
        protected CheckoutItemTranslation() { } // EF

        public CheckoutItemTranslation(long checkoutItemId, long languageId, string productName)
        {
            if (checkoutItemId <= 0)
                throw new DomainException("CheckoutItemId is required.");
            if (languageId <= 0)
                throw new DomainException("LanguageId is required.");
            if (string.IsNullOrWhiteSpace(productName))
                throw new DomainException("ProductName is required.");

            CheckoutItemId = checkoutItemId;
            WebSiteLanguageId = languageId;
            ProductName = productName.Trim();
        }

        public long CheckoutItemId { get; private set; }
        public CheckoutItem CheckoutItem { get; private set; }

        public long WebSiteLanguageId { get; private set; }

        public string ProductName { get; private set; }

        public void Update(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new DomainException("ProductName is required.");

            ProductName = productName.Trim();
        }
    }
}
