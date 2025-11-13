using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Orders;
using CMS.Domain.Users.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CMS.Domain.Shipments
{
    public class Shipment : AggregateRoot
    {
        private readonly List<ShipmentItem> _items = new();
        public IReadOnlyCollection<ShipmentItem> Items => _items.AsReadOnly();

        private readonly List<ShipmentTranslation> _translations = new();
        public IReadOnlyCollection<ShipmentTranslation> Translations => _translations.AsReadOnly();

        protected Shipment() { } // EF

        public Shipment(long orderId, long createdById, string trackingNumber, long? vendorId = null)
        {
            if (orderId <= 0)
                throw new DomainException("OrderId is required.");
            if (createdById <= 0)
                throw new DomainException("CreatedById is required.");

            OrderId = orderId;
            CreatedById = createdById;
            VendorId = vendorId;
            TrackingNumber = trackingNumber?.Trim();

            CreatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedOn = CreatedOn;
        }

        public long OrderId { get; private set; }
        public Order Order { get; private set; }

        [StringLength(450)]
        public string TrackingNumber { get; private set; }

        public long? VendorId { get; private set; }

        public long CreatedById { get; private set; }
        public AppUser CreatedBy { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset LatestUpdatedOn { get; private set; }

        // --- Behaviors ---

        public void UpdateTrackingNumber(string trackingNumber)
        {
            TrackingNumber = trackingNumber?.Trim();
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void AddItem(long orderItemId, int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            _items.Add(new ShipmentItem(Id, orderItemId, quantity));
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        public void RemoveItem(long shipmentItemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == shipmentItemId);
            if (item == null)
                throw new DomainException("Shipment item not found.");

            _items.Remove(item);
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        // --- Translation behaviors ---
        public void AddTranslation(long languageId, string trackingNumber)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new ShipmentTranslation(Id, languageId, trackingNumber));
        }

        public void UpdateTranslation(long languageId, string trackingNumber)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(trackingNumber);
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
