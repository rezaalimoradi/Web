using CMS.Domain.Common;
using CMS.Domain.Localization.Entities;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Shipments
{
    public class ShipmentTranslation : BaseEntity
    {
        protected ShipmentTranslation() { } // EF

        public ShipmentTranslation(long shipmentId, long webSiteLanguageId, string trackingNumber)
        {
            ShipmentId = shipmentId;
            WebSiteLanguageId = webSiteLanguageId;
            Update(trackingNumber);
        }

        public long ShipmentId { get; private set; }
        public Shipment Shipment { get; private set; }

        public long WebSiteLanguageId { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

        public string TrackingNumber { get; private set; }

        public void Update(string trackingNumber)
        {
            TrackingNumber = trackingNumber?.Trim();
        }
    }
}
