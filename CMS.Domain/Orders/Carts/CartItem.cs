using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Domain.Orders.Carts
{
    public class CartItem : BaseEntity
    {
        private readonly List<CartItemTranslation> _translations = new();

        protected CartItem() { }

        public CartItem(long cartId, long productId, int quantity, decimal unitPrice, decimal? discount = null)
        {
            if (cartId <= 0) throw new DomainException("Invalid CartId");
            if (productId <= 0) throw new DomainException("Invalid ProductId");
            if (quantity <= 0) throw new DomainException("Quantity must be greater than 0");
            if (unitPrice < 0) throw new DomainException("UnitPrice cannot be negative");
            if (discount < 0) throw new DomainException("Discount cannot be negative");
            if (discount > unitPrice) throw new DomainException("Discount cannot exceed UnitPrice");

            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount ?? 0m;
        }

        #region Properties

        public long CartId { get; private set; }
        public long ProductId { get; private set; }
        public Product Product { get; private set; }

        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }

        public Cart Cart { get; private set; }

        public IReadOnlyCollection<CartItemTranslation> Translations => _translations.AsReadOnly();

        public decimal TotalPrice => (UnitPrice - Discount) * Quantity;

        #endregion

        #region CRUD - Item Properties

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0) throw new DomainException("Quantity must be greater than 0");
            Quantity = quantity;
        }

        public void UpdatePrice(decimal unitPrice, decimal? discount = null)
        {
            if (unitPrice < 0) throw new DomainException("UnitPrice cannot be negative");
            if (discount < 0) throw new DomainException("Discount cannot be negative");
            if (discount > unitPrice) throw new DomainException("Discount cannot exceed UnitPrice");

            UnitPrice = unitPrice;
            Discount = discount ?? 0m;
        }

        #endregion

        #region CRUD - Translations

        public void AddTranslation(long languageId, string title)
        {
            if (_translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation already exists");

            _translations.Add(new CartItemTranslation(Id, languageId, title));
        }

        public void UpdateTranslation(long languageId, string title)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                ?? throw new DomainException("Translation not found");

            translation.Update(title);
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = _translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);
            if (translation != null)
            {
                _translations.Remove(translation);
            }
        }

        public static CartItem CreateTemporary(long productId, int quantity, decimal unitPrice, decimal? discount = null)
        {
            if (productId <= 0) throw new DomainException("Invalid ProductId");
            if (quantity <= 0) throw new DomainException("Quantity must be greater than 0");
            if (unitPrice < 0) throw new DomainException("UnitPrice cannot be negative");
            if (discount < 0) throw new DomainException("Discount cannot be negative");
            if (discount > unitPrice) throw new DomainException("Discount cannot exceed UnitPrice");

            var item = new CartItem
            {
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                Discount = discount ?? 0m
            };
            return item;
        }

        #endregion
    }
}
