using CMS.Domain.Common;
using CMS.Domain.Localization.Entities;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Shippings
{
    public class ShippingTranslation : BaseEntity
    {
        protected ShippingTranslation() { } // EF

        public ShippingTranslation(long shippingId, long languageId, string name, string description = null)
        {
            ShippingId = shippingId;
            WebSiteLanguageId = languageId;
            Name = name?.Trim() ?? throw new ArgumentNullException(nameof(name));
            Description = description?.Trim();
        }

        public long ShippingId { get; private set; }
        public Shipping Shipping { get; private set; }

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
