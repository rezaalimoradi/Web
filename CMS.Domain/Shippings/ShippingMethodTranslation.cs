using CMS.Domain.Common;
using CMS.Domain.Localization.Entities;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Shippings
{
    public class ShippingMethodTranslation : BaseEntity
    {
        protected ShippingMethodTranslation() { }

        public ShippingMethodTranslation(long methodId, long languageId, string name, string description = null)
        {
            ShippingMethodId = methodId;
            WebSiteLanguageId = languageId;
            Name = name?.Trim() ?? throw new ArgumentNullException(nameof(name));
            Description = description?.Trim();
        }

        public long ShippingMethodId { get; private set; }
        public ShippingMethod ShippingMethod { get; private set; }

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
