using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Domain.Shipments
{
    public class ShipmentItem : BaseEntity
    {
        private readonly List<ShipmentItemTranslation> _translations = new();
        public IReadOnlyCollection<ShipmentItemTranslation> Translations => _translations.AsReadOnly();

        protected ShipmentItem() { } // EF

        public ShipmentItem(long shipmentId, long orderItemId, int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            ShipmentId = shipmentId;
            OrderItemId = orderItemId;
            Quantity = quantity;
        }


        public long ShipmentId { get; private set; }
        public Shipment Shipment { get; private set; }

        public long OrderItemId { get; private set; }

        public long ProductId { get; private set; }
        public Product Product { get; private set; }

        public int Quantity { get; private set; }

        // --- Behaviors ---
        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            Quantity = quantity;
        }

        public void AddTranslation(long languageId, string label, string note = null)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new ShipmentItemTranslation(Id, languageId, label, note));
        }

        public void UpdateTranslation(long languageId, string label, string note = null)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(label, note);
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }
    }
}
