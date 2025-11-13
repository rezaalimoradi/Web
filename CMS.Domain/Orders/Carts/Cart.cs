using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Domain.Orders.Carts
{
    public class Cart : AggregateRoot
    {
        private readonly List<CartItem> _items = new();
        private readonly List<CartTranslation> _translations = new();

        protected Cart() { }

        public Cart(long webSiteId, string customerIdentifier)
        {
            if (webSiteId <= 0) throw new DomainException("Invalid WebSiteId");
            if (string.IsNullOrWhiteSpace(customerIdentifier)) throw new DomainException("CustomerIdentifier is required");

            WebSiteId = webSiteId;
            CustomerIdentifier = customerIdentifier;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        #region Properties

        public long WebSiteId { get; private set; }
        public WebSite WebSite { get; private set; }

        public string CustomerIdentifier { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
        public IReadOnlyCollection<CartTranslation> Translations => _translations.AsReadOnly();

        public decimal TotalPrice => _items.Sum(i => i.TotalPrice);

        #endregion

        #region Private Helpers

        private void Touch() => UpdatedAt = DateTime.UtcNow;

        #endregion

        #region CRUD - Items

        public void AddItem(long productId, int quantity, decimal unitPrice, decimal? discount = null)
        {
            if (productId <= 0) throw new DomainException("Invalid ProductId");
            if (quantity <= 0) throw new DomainException("Quantity must be greater than 0");
            if (unitPrice < 0) throw new DomainException("UnitPrice cannot be negative");

            var existing = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.UpdateQuantity(existing.Quantity + quantity);
            }
            else
            {
                // ✅ از متد امن استفاده می‌کنیم تا CartId لازم نباشد
                var item = CartItem.CreateTemporary(productId, quantity, unitPrice, discount);
                _items.Add(item);
            }

            Touch();
        }

        public void UpdateItemQuantity(long productId, int quantity)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId)
                ?? throw new DomainException("Item not found");

            item.UpdateQuantity(quantity);
            Touch();
        }

        public void UpdateItemPrice(long productId, decimal unitPrice, decimal? discount = null)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId)
                ?? throw new DomainException("Item not found");

            item.UpdatePrice(unitPrice, discount);
            Touch();
        }

        public void RemoveItem(long productId)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _items.Remove(item);
                Touch();
            }
        }

        public void ClearItems()
        {
            _items.Clear();
            Touch();
        }

        #endregion

        #region CRUD - Translations

        public void AddTranslation(long languageId, string title, string? description)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation already exists");

            _translations.Add(new CartTranslation(Id, languageId, title, description));
            Touch();
        }

        public void UpdateTranslation(long languageId, string title, string? description)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                ?? throw new DomainException("Translation not found");

            translation.Update(title, description);
            Touch();
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation != null)
            {
                _translations.Remove(translation);
                Touch();
            }
        }

        public void MergeWithUser(string userIdentifier)
        {
            if (string.IsNullOrWhiteSpace(userIdentifier))
                throw new ArgumentException("User identifier is invalid.", nameof(userIdentifier));

            CustomerIdentifier = userIdentifier;
        }

        #endregion

        #region Update CustomerIdentifier

        public void UpdateCustomerIdentifier(string newCustomerIdentifier)
        {
            if (string.IsNullOrWhiteSpace(newCustomerIdentifier))
                throw new DomainException("CustomerIdentifier نمی‌تواند خالی باشد.");

            // اگر Identifier قبلی با جدید متفاوت است، آپدیت کن
            if (CustomerIdentifier != newCustomerIdentifier)
            {
                CustomerIdentifier = newCustomerIdentifier;
                Touch(); // زمان آخرین بروزرسانی
            }
        }

        #endregion

    }
}
