using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Catalog.Entities
{
    public class BrandTranslation : BaseEntity
    {
        public long BrandId { get; private set; }
        public Brand Brand { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }
        public string Slug { get; set; }

        public long WebSiteLanguageId { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; set; }

        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public string? CanonicalUrl { get; set; }

        // سازنده محافظت‌شده برای EF Core
        protected BrandTranslation() { }

        // سازنده اصلی با اعتبارسنجی
        internal BrandTranslation(
            long brandId,
            string title,
            string? description,
            string slug,
            long languageId,
            string? metaTitle,
            string? metaDescription,
            string? metaKeywords,
            string? canonicalUrl
            )
        {
            if (languageId <= 0) throw new DomainException("Invalid LanguageId.");
            if (string.IsNullOrWhiteSpace(title)) throw new DomainException("Title is required.");
            if (string.IsNullOrWhiteSpace(slug)) throw new DomainException("Slug is required.");

            BrandId = brandId;
            Title = title;
            Description = description;
            Slug = slug;
            WebSiteLanguageId = languageId;
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            MetaKeywords = metaKeywords;
            CanonicalUrl = canonicalUrl;
        }

        // متد آپدیت محتوا
        public void Update(
            string title,
            string? description,
            string slug,
            string? metaTitle,
            string? metaDescription,
            string? metaKeywords,
            string? canonicalUrl
            )
        {
            if (string.IsNullOrWhiteSpace(title)) throw new DomainException("Title is required.");
            if (string.IsNullOrWhiteSpace(slug)) throw new DomainException("Slug is required.");

            Title = title;
            Description = description;
            Slug = slug;
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            MetaKeywords = metaKeywords;
            CanonicalUrl = canonicalUrl;
        }

        // متد آپدیت متادیتا
        public void UpdateMeta(string? metaTitle, string? metaDescription, string? metaKeywords, string? canonicalUrl)
        {
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            MetaKeywords = metaKeywords;
            CanonicalUrl = canonicalUrl;
        }
    }
}
