using CMS.Domain.Common;
using CMS.Domain.Localization.Entities;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Shippings
{
    public class ShippingFreeTranslation : BaseEntity
    {
        protected ShippingFreeTranslation() { } // EF

        public ShippingFreeTranslation(
            long shippingFreeId,
            long webSiteLanguageId,
            string name,
            string description = null
        )
        {
            ShippingFreeId = shippingFreeId;
            WebSiteLanguageId = webSiteLanguageId;
            Name = name?.Trim() ?? throw new ArgumentNullException(nameof(name));
            Description = description?.Trim();
        }

        public long ShippingFreeId { get; private set; }
        public ShippingFree ShippingFree { get; private set; }

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
