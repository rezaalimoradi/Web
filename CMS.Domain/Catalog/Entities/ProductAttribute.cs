using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Catalog.Entities
{
    /// <summary>
    /// تعریف ویژگی محصول (مثل رنگ، سایز، حجم و ...)
    /// </summary>
    public class ProductAttribute : AggregateRoot
    {
        #region Properties

        public long WebSiteId { get; set; }
        public virtual WebSite WebSite { get; private set; } = default!;

        /// <summary>
        /// آیا این ویژگی اجازه‌ی چند مقدار دارد؟ (مثل "چند رنگ" یا "چند سایز")
        /// </summary>
        public bool AllowMultipleValues { get; private set; }

        public virtual ICollection<ProductAttributeTranslation> Translations { get; private set; } = new List<ProductAttributeTranslation>();
        public virtual ICollection<ProductAttributeValue> Values { get; private set; } = new List<ProductAttributeValue>();

        public ICollection<ProductProductAttribute> ProductProductAttributes { get; private set; } = new HashSet<ProductProductAttribute>();


        #endregion

        #region Constructors

        protected ProductAttribute() { }

        public ProductAttribute(long webSiteId, long languageId, string name, bool allowMultiple = false)
        {
            if (webSiteId <= 0)
                throw new DomainException("Invalid WebSiteId.");
            if (languageId <= 0)
                throw new DomainException("Invalid languageId.");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Attribute name is required.");

            WebSiteId = webSiteId;
            AllowMultipleValues = allowMultiple;
        }


        #endregion

        #region Behaviors

        public void SetAllowMultipleValues(bool allow) => AllowMultipleValues = allow;

        // ---------- Translation Behaviors ----------
        public void AddTranslation(long languageId, string name)
        {
            if (languageId <= 0)
                throw new DomainException("Invalid languageId.");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Translation name is required.");
            if (Translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            Translations.Add(new ProductAttributeTranslation(this, languageId, name));
        }


        public void UpdateTranslation(long languageId, string name)
        {
            var translation = Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                ?? throw new DomainException("Translation not found.");

            translation.Update(name);
        }

        public void RemoveTranslation(long languageId)
        {
            var translation = Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                ?? throw new DomainException("Translation not found.");

            Translations.Remove(translation);
        }

        // ---------- Value Behaviors ----------
        public ProductAttributeValue AddValue(long languageId, string value,
            decimal? priceAdjustment = null, int? stockQuantity = null, int displayOrder = 0)
        {
            if (languageId <= 0)
                throw new DomainException("Invalid languageId.");
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Value is required.");

            var newValue = new ProductAttributeValue(this, languageId, value, priceAdjustment, stockQuantity, displayOrder);
            Values.Add(newValue);
            return newValue;
        }


        public void RemoveValue(long valueId)
        {
            var value = Values.FirstOrDefault(v => v.Id == valueId)
                ?? throw new DomainException("Attribute value not found.");

            Values.Remove(value);
        }

        public void UpdateValueTranslation(long valueId, long languageId, string newValue)
        {
            var attrValue = Values.FirstOrDefault(v => v.Id == valueId)
                ?? throw new DomainException("Attribute value not found.");

            attrValue.UpdateTranslation(languageId, newValue);
        }

        #endregion
    }
}
