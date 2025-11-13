using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Catalog.Entities
{
    /// <summary>
    /// ترجمه‌ی یک محصول برای زبان خاص
    /// </summary>
    public class ProductTranslation : BaseEntity
    {
        #region Properties

        public long ProductId { get; private set; }
        public virtual Product Product { get; private set; } = default!;

        public long WebSiteLanguageId { get; private set; }
        public virtual WebSiteLanguage WebSiteLanguage { get; private set; } = default!;

        public string Name { get; private set; } = default!;
        public string? SecondName { get; private set; }
        public string? ShortDescription { get; private set; }
        public string Description { get; private set; } = default!;
        public string Slug { get; private set; } = default!;
        public string? CanonicalUrl { get; private set; }
        public string? MetaTitle { get; private set; }
        public string? MetaDescription { get; private set; }
        public string? MetaKeywords { get; private set; }

        #endregion

        #region Constructors

        protected ProductTranslation() { } // For EF

        internal ProductTranslation(
            long productId,
            long webSiteLanguageId,
            string name,
            string description,
            string slug,
            string? secondName = null,
            string? shortDescription = null,
            string? metaTitle = null,
            string? metaDescription = null,
            string? metaKeywords = null,
            string? canonicalUrl = null)
        {
            //if (productId <= 0)
            //    throw new DomainException("Invalid ProductId.");
            if (webSiteLanguageId <= 0)
                throw new DomainException("Invalid WebSiteLanguageId.");
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Description cannot be empty.");
            if (string.IsNullOrWhiteSpace(slug))
                throw new DomainException("Slug cannot be empty.");

            ProductId = productId;
            WebSiteLanguageId = webSiteLanguageId;

            Update(name, description, slug, secondName, shortDescription, metaTitle, metaDescription, metaKeywords, canonicalUrl);
        }

        #endregion

        #region Behaviors

        public void Update(
            string name,
            string description,
            string slug,
            string? secondName = null,
            string? shortDescription = null,
            string? metaTitle = null,
            string? metaDescription = null,
            string? metaKeywords = null,
            string? canonicalUrl = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Description cannot be empty.");
            if (string.IsNullOrWhiteSpace(slug))
                throw new DomainException("Slug cannot be empty.");

            Name = name.Trim();
            Description = description.Trim();
            Slug = slug.Trim();
            SecondName = secondName?.Trim();
            ShortDescription = shortDescription?.Trim();
            MetaTitle = metaTitle?.Trim();
            MetaDescription = metaDescription?.Trim();
            MetaKeywords = metaKeywords?.Trim();
            CanonicalUrl = canonicalUrl?.Trim();
        }

        public void UpdateSEO(string? metaTitle, string? metaDescription, string? metaKeywords)
        {
            MetaTitle = metaTitle?.Trim();
            MetaDescription = metaDescription?.Trim();
            MetaKeywords = metaKeywords?.Trim();
        }

        public void UpdateSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new DomainException("Slug cannot be empty.");

            Slug = slug.Trim();
        }

        #endregion
    }
}
