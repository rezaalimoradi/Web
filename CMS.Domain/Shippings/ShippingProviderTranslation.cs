using CMS.Domain.Common;
using CMS.Domain.Localization.Entities;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Shippings
{
    public class ShippingProviderTranslation : BaseEntity
    {
        protected ShippingProviderTranslation() { }

        public ShippingProviderTranslation(long providerId, long languageId, string name, string description = null)
        {
            ShippingProviderId = providerId;
            WebSiteLanguageId = languageId;
            Name = name?.Trim() ?? throw new ArgumentNullException(nameof(name));
            Description = description?.Trim();
        }

        public long ShippingProviderId { get; private set; }
        public ShippingProvider ShippingProvider { get; private set; }

        public long WebSiteLanguageId { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

        public string Name { get; private set; }
        public string? Description { get; private set; }

        public void Update(string name, string description = null)
        {
            Name = name?.Trim() ?? throw new ArgumentNullException(nameof(name));
            Description = description?.Trim();
        }
    }
}
