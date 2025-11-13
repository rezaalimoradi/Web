using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Users.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CMS.Domain.Checkouts
{
    public class Checkout : AggregateRoot
    {
        private readonly List<CheckoutItem> _items = new();
        public IReadOnlyCollection<CheckoutItem> Items => _items.AsReadOnly();

        private readonly List<CheckoutTranslation> _translations = new();
        public IReadOnlyCollection<CheckoutTranslation> Translations => _translations.AsReadOnly();

        protected Checkout() { } // For EF

        public Checkout(long customerId, long createdById)
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

        [StringLength(450)]
        public string CouponCode { get; private set; }

        [StringLength(450)]
        public string CouponRuleName { get; private set; }

        [StringLength(450)]
        public string ShippingMethod { get; private set; }

        public bool IsProductPriceIncludeTax { get; private set; }

        public decimal? ShippingAmount { get; private set; }
        public decimal? TaxAmount { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Total { get; private set; }

        public long? VendorId { get; private set; }

        /// <summary>
        /// Json serialized of shipping form
        /// </summary>
        public string ShippingData { get; private set; }

        [StringLength(1000)]
        public string OrderNote { get; private set; }

        // --- Behaviors ---

        #region Items
        public void AddItem(long productId, decimal unitPrice, int quantity = 1)
        {
            if (unitPrice < 0)
                throw new DomainException("Unit price cannot be negative.");
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            var existing = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.UpdateQuantity(existing.Quantity + quantity);
            }
            else
            {
                var item = new CheckoutItem(productId, unitPrice, quantity);
                _items.Add(item);
            }

            RecalculateTotals();
        }

        public void RemoveItem(long itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new DomainException("Item not found in checkout.");

            _items.Remove(item);
            RecalculateTotals();
        }

        public void ClearItems()
        {
            _items.Clear();
            RecalculateTotals();
        }
        #endregion

        #region Coupon
        public void ApplyCoupon(string code, string ruleName)
        {
            CouponCode = code?.Trim();
            CouponRuleName = ruleName?.Trim();
        }

        public void RemoveCoupon()
        {
            CouponCode = null;
            CouponRuleName = null;
        }
        #endregion

        #region Translations
        public void AddTranslation(long languageId, string orderNote, string shippingMethod, string paymentMethod)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            _translations.Add(new CheckoutTranslation(Id, languageId, orderNote, shippingMethod, paymentMethod));
        }

        public void UpdateTranslation(long languageId, string orderNote, string shippingMethod, string paymentMethod)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            translation.Update(orderNote, shippingMethod, paymentMethod);
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }
        #endregion

        private void RecalculateTotals()
        {
            SubTotal = _items.Sum(i => i.UnitPrice * i.Quantity);
            Total = SubTotal + (ShippingAmount ?? 0) + (TaxAmount ?? 0);
            LatestUpdatedOn = DateTimeOffset.UtcNow;
        }
    }
}
