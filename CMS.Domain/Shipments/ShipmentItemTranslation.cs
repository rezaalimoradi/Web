using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Shipments
{
    public class ShipmentItemTranslation : BaseEntity
    {
        protected ShipmentItemTranslation() { }

        public long ShipmentItemId { get; private set; }
        public ShipmentItem ShipmentItem { get; private set; }

        public long WebSiteLanguageId { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

        public string Label { get; private set; }
        public string Note { get; private set; }

        internal ShipmentItemTranslation(long shipmentItemId, long languageId, string label, string note = null)
        {
            ShipmentItemId = shipmentItemId;
            WebSiteLanguageId = languageId;
            Label = label?.Trim() ?? throw new ArgumentNullException(nameof(label));
            Note = note?.Trim();
        }

        public void Update(string label, string note = null)
        {
            Label = label?.Trim() ?? throw new ArgumentNullException(nameof(label));
            Note = note?.Trim();
        }
    }
}
