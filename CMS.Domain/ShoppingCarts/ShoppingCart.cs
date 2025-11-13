using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Domain.ShoppingCarts
{
    public class ShoppingCart : AggregateRoot
    {
        private readonly List<ShoppingCartItem> _items = new();
        public IReadOnlyCollection<ShoppingCartItem> Items => _items.AsReadOnly();

        private readonly List<ShoppingCartTranslation> _translations = new();
        public IReadOnlyCollection<ShoppingCartTranslation> Translations => _translations.AsReadOnly();

        protected ShoppingCart() { } // EF

        public ShoppingCart(long customerId, long createdById)
        {
            if (customerId <= 0)
                throw new DomainException("CustomerId is required.");

            CustomerId = customerId;
            CreatedById = createdById;

            CreatedOn = DateTimeOffset.UtcNow;
            LatestUpdatedOn = CreatedOn;
        }

        public long CustomerId { get; private set; }
        public AppUser Customer { get; private set; }

        public long CreatedById { get; private set; }
        public AppUser CreatedBy { get; private set; }

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset LatestUpdatedOn { get; private set; }

        public decimal SubTotal { get; private set; }

        // --- Behaviors ---

        public void AddItem(long productId, decimal unitPrice, int quantity = 1)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");
            if (unitPrice < 0)
                throw new DomainException("Unit price cannot be negative.");

            var existing = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.UpdateQuantity(existing.Quantity + quantity);
            }
            else
            {
                var item = new ShoppingCartItem(Id, productId, unitPrice, quantity);
                _items.Add(item);
            }

            RecalculateTotals();
        }

        public void RemoveItem(long itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new DomainException("Item not found in cart.");

            _items.Remove(item);
            RecalculateTotals();
        }

        public void Clear()
        {
            _items.Clear();
            SubTotal = 0;
        }

        private void RecalculateTotals()
        {
            SubTotal = _items.Sum(i => i.UnitPrice * i.Quantity);
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }

        // --- Translation behaviors ---

        public void AddTranslation(long languageId, string note)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new ShoppingCartTranslation(Id, languageId, note));
        }

        public void UpdateTranslation(long languageId, string note)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(note);
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
