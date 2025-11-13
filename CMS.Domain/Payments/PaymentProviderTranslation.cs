using CMS.Domain.Common;
using CMS.Domain.Localization.Entities;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Payments
{
    public class PaymentProviderTranslation : BaseEntity
    {
        protected PaymentProviderTranslation() { } // For EF

        public PaymentProviderTranslation(
            long paymentProviderId,
            long webSiteLanguageId,
            string name,
            string configureUrl,
            string landingViewComponentName)
        {
            PaymentProviderId = paymentProviderId;
            WebSiteLanguageId = webSiteLanguageId;
            Update(name, configureUrl, landingViewComponentName);
        }

        public long PaymentProviderId { get; private set; }
        public PaymentProvider PaymentProvider { get; private set; }

        public long WebSiteLanguageId { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

        public string Name { get; private set; }
        public string ConfigureUrl { get; private set; }
        public string LandingViewComponentName { get; private set; }

        public void Update(string name, string configureUrl, string landingViewComponentName)
        {
            Name = name?.Trim();
            ConfigureUrl = configureUrl?.Trim();
            LandingViewComponentName = landingViewComponentName?.Trim();
        }
    }
}
