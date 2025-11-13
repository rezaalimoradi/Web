using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Catalog.Entities
{
    public class ProductCategoryTranslation : BaseEntity
    {
        public long ProductCategoryId { get; private set; }
        public ProductCategory ProductCategory { get; set; }

        public long WebSiteLanguageId { get; set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

        public string Title { get; set; }
        public string? Description { get; set; }
        public string Slug { get; set; }

        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public string? CanonicalUrl { get; set; }

        // سازنده محافظت‌شده برای EF Core
        public ProductCategoryTranslation() { }

        // سازنده اصلی با اعتبارسنجی
        internal ProductCategoryTranslation(
            long productCategoryId,
            string title,
            string? description,
            string slug,
            long webSiteLanguageId,
            string? metaTitle = null,
            string? metaDescription = null,
            string? metaKeywords = null,
            string? canonicalUrl = null)
        {
            if (webSiteLanguageId <= 0)
                throw new DomainException("Invalid languageId.");
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title is required.");
            if (string.IsNullOrWhiteSpace(slug))
                throw new DomainException("Slug is required.");

            ProductCategoryId = productCategoryId;
            WebSiteLanguageId = webSiteLanguageId;
            Title = title;
            Description = description;
            Slug = slug;

            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            MetaKeywords = metaKeywords;
            CanonicalUrl = canonicalUrl;
        }

        public static ProductCategoryTranslation Create(
            long productCategoryId,
            long webSiteLanguageId,
            string title,
            string slug,
            string? description = null,
            string? metaTitle = null,
            string? metaDescription = null,
            string? metaKeywords = null,
            string? canonicalUrl = null)
        {
            return new ProductCategoryTranslation(
                productCategoryId: productCategoryId,
                title: title,
                description: description,
                slug: slug,
                webSiteLanguageId: webSiteLanguageId,
                metaTitle: metaTitle,
                metaDescription: metaDescription,
                metaKeywords: metaKeywords,
                canonicalUrl: canonicalUrl
            );
        }

        // متد آپدیت ترجمه
        public void Update(string title, string? description, string slug)
        {
            Title = title?.Trim() ?? throw new DomainException("Title is required.");
            Slug = slug?.Trim().ToLowerInvariant() ?? throw new DomainException("Slug is required.");
            Description = description?.Trim();

            if (Title.Length > 200) throw new DomainException("Title too long.");
            if (Slug.Length > 250) throw new DomainException("Slug too long.");
        }

        // متد آپدیت Metadata
        public void UpdateMeta(
                    string? metaTitle,
                    string? metaDescription,
                    string? metaKeywords,
                    string? canonicalUrl)
        {
            MetaTitle = metaTitle?.Trim();
            MetaDescription = metaDescription?.Trim();
            MetaKeywords = metaKeywords?.Trim();
            CanonicalUrl = canonicalUrl?.Trim();
        }

        public void GenerateSlugFromTitle()
        {
            if (string.IsNullOrWhiteSpace(Title)) return;

            var slug = Title
                .Trim()
                .ToLowerInvariant()
                .Replace(" ", "-")
                .Replace("--", "-");

            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\-]", "");
            slug = slug.Trim('-');
            if (slug.Length > 230) slug = slug.Substring(0, 230);

            Slug = slug;
        }
    }
}
