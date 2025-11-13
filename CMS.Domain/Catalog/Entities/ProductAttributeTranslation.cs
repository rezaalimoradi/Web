using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Catalog.Entities
{
    /// <summary>
    /// ترجمه‌ی ویژگی محصول برای یک زبان خاص (مثلاً "رنگ" / "Color")
    /// </summary>
    public class ProductAttributeTranslation : BaseEntity
    {
        #region Properties

        public long ProductAttributeId { get; private set; }
        public virtual ProductAttribute ProductAttribute { get; private set; } = default!;

        public long WebSiteLanguageId { get; private set; }
        public virtual WebSiteLanguage WebSiteLanguage { get; private set; } = default!;

        public string Name { get; private set; } = default!;

        #endregion

        #region Constructors

        protected ProductAttributeTranslation() { } // EF Only

        internal ProductAttributeTranslation(ProductAttribute productAttribute, long webSiteLanguageId, string name)
        {
            ProductAttribute = productAttribute ?? throw new DomainException("Invalid ProductAttribute.");
            if (webSiteLanguageId <= 0)
                throw new DomainException("Invalid WebSiteLanguageId.");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be empty.");

            WebSiteLanguageId = webSiteLanguageId;
            Name = name.Trim();
        }


        #endregion

        #region Behaviors

        /// <summary>
        /// بروزرسانی مقدار ترجمه (با trim و validation)
        /// </summary>
        /// <param name="name">نام جدید ویژگی</param>
        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be empty.");

            Name = name.Trim();
        }

        /// <summary>
        /// غیرفعال‌سازی ترجمه بدون حذف فیزیکی (برای آینده، اگر ترجمه‌ها نسخه‌دار شوند)
        /// </summary>
        public void Deactivate()
        {
            // placeholder behavior for future logic (soft-delete, versioning, etc.)
        }

        #endregion
    }
}
