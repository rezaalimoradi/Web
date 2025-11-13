using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CMS.Domain.ShoppingCarts
{
    public class ShoppingCartItem : BaseEntity
    {
        private readonly List<ShoppingCartItemTranslation> _translations = new();
        public IReadOnlyCollection<ShoppingCartItemTranslation> Translations => _translations.AsReadOnly();

        protected ShoppingCartItem() { } // EF

        public ShoppingCartItem(long cartId, long productId, decimal unitPrice, int quantity)
        {
            if (cartId <= 0)
                throw new DomainException("CartId is required.");
            if (productId <= 0)
                throw new DomainException("ProductId is required.");
            if (unitPrice < 0)
                throw new DomainException("Unit price cannot be negative.");
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            ShoppingCartId = cartId;
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public long ShoppingCartId { get; private set; }
        [JsonIgnore]
        public ShoppingCart ShoppingCart { get; private set; }

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

        // --- Translation behaviors ---

        public void AddTranslation(long languageId, string name, string description)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new ShoppingCartItemTranslation(Id, languageId, name, description));
        }

        public void UpdateTranslation(long languageId, string name, string description)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(name, description);
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
