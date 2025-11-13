using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Catalog.Entities
{
    /// <summary>
    /// ترجمه‌ی مقدار یک ویژگی محصول برای زبان خاص (مثلاً "قرمز" / "Red")
    /// </summary>
    public class ProductAttributeValueTranslation : BaseEntity
    {
        #region Properties

        public long ProductAttributeValueId { get; private set; }
        public virtual ProductAttributeValue ProductAttributeValue { get; private set; } = default!;

        public long WebSiteLanguageId { get; private set; }
        public virtual WebSiteLanguage WebSiteLanguage { get; private set; } = default!;

        public string Value { get; private set; } = default!;

        #endregion

        #region Constructors

        protected ProductAttributeValueTranslation() { } // EF Only

        internal ProductAttributeValueTranslation(ProductAttributeValue value, long webSiteLanguageId, string translatedValue)
        {
            ProductAttributeValue = value ?? throw new DomainException("Invalid ProductAttributeValue.");
            if (webSiteLanguageId <= 0)
                throw new DomainException("Invalid WebSiteLanguageId.");
            if (string.IsNullOrWhiteSpace(translatedValue))
                throw new DomainException("Value cannot be empty.");

            WebSiteLanguageId = webSiteLanguageId;
            Value = translatedValue.Trim();
        }


        #endregion

        #region Behaviors

        /// <summary>
        /// بروزرسانی مقدار ترجمه (با اعتبارسنجی)
        /// </summary>
        /// <param name="value">مقدار جدید ویژگی</param>
        public void Update(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Value cannot be empty.");

            Value = value.Trim();
        }

        /// <summary>
        /// غیرفعال‌سازی ترجمه بدون حذف فیزیکی (در صورت نیاز به soft delete)
        /// </summary>
        public void Deactivate()
        {
            // Reserved for future soft-delete or versioning logic
        }

        #endregion
    }
}
