using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CMS.Domain.Checkouts
{
    public class CheckoutItem : BaseEntity
    {
        private readonly List<CheckoutItemTranslation> _translations = new();
        public IReadOnlyCollection<CheckoutItemTranslation> Translations => _translations.AsReadOnly();

        protected CheckoutItem() { } // EF

        public CheckoutItem(long productId, decimal unitPrice, int quantity)
        {
            if (productId <= 0)
                throw new DomainException("ProductId is required.");
            if (unitPrice < 0)
                throw new DomainException("Unit price cannot be negative.");
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public long CheckoutId { get; private set; }
        [JsonIgnore]
        public Checkout Checkout { get; private set; }

        public long ProductId { get; private set; }
        [JsonIgnore]
        public Product Product { get; private set; }

        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public decimal Total => UnitPrice * Quantity;

        // --- Behaviors ---

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");
            Quantity = newQuantity;
        }

        public void AddTranslation(long languageId, string productName)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new CheckoutItemTranslation(Id, languageId, productName));
        }

        public void UpdateTranslation(long languageId, string productName)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(productName);
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
