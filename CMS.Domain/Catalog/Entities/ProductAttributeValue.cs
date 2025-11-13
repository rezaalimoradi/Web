using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Catalog.Entities
{
    public class ProductAttributeValue : BaseEntity
    {
        #region Properties
        public long ProductAttributeId { get; private set; }
        public virtual ProductAttribute ProductAttribute { get; private set; } = default!;

        public decimal? PriceAdjustment { get; private set; }
        public int? StockQuantity { get; private set; }
        public bool IsActive { get; private set; } = true;
        public int DisplayOrder { get; private set; }

        public ICollection<ProductAttributeValueTranslation> Translations { get; private set; } = new List<ProductAttributeValueTranslation>();
        public ICollection<ProductProductAttribute_ValueMapping> ProductProductAttribute_Values { get; private set; } = new List<ProductProductAttribute_ValueMapping>();
        #endregion

        #region Constructors

        protected ProductAttributeValue() { } // EF only

        internal ProductAttributeValue(
            ProductAttribute productAttribute,
            long languageId,
            string value,
            decimal? priceAdjustment = null,
            int? stockQuantity = null,
            int displayOrder = 0)
        {
            ProductAttribute = productAttribute ?? throw new DomainException("Invalid ProductAttribute.");
            if (languageId <= 0)
                throw new DomainException("Invalid languageId.");
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Value is required.");

            SetPriceAdjustment(priceAdjustment);
            SetStockQuantity(stockQuantity);
            SetDisplayOrder(displayOrder);

            // 🔹 ترجمه اولیه
            Translations.Add(new ProductAttributeValueTranslation(this, languageId, value));
        }

        #endregion

        #region Translation Behaviors

        public void AddTranslation(long languageId, string value)
        {
            if (languageId <= 0)
                throw new DomainException("Invalid languageId.");
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Value is required.");
            if (Translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            Translations.Add(new ProductAttributeValueTranslation(this, languageId, value));
        }

        public void UpdateTranslation(long languageId, string value)
        {
            var translation = Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                ?? throw new DomainException("Translation not found.");
            translation.Update(value);
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                ?? throw new DomainException("Translation not found.");
            Translations.Remove(translation);
        }

        #endregion

        #region Business Behaviors
        public void SetPriceAdjustment(decimal? adjustment)
        {
            if (adjustment is < 0)
                throw new DomainException("Price adjustment cannot be negative.");
            PriceAdjustment = adjustment;
        }

        public void SetStockQuantity(int? quantity)
        {
            if (quantity is < 0)
                throw new DomainException("Stock quantity cannot be negative.");
            StockQuantity = quantity;
        }

        public void SetDisplayOrder(int order)
        {
            if (order < 0)
                throw new DomainException("Display order cannot be negative.");
            DisplayOrder = order;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
        #endregion
    }

}
