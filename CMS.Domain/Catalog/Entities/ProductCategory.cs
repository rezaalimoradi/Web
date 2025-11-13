using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Media.Entities;
using CMS.Domain.Tenants.Entitis;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CMS.Domain.Catalog.Entities
{
    public class ProductCategory : AggregateRoot
    {
        // ---------- Basic ----------
        public long WebSiteId { get; private set; }

        [ForeignKey(nameof(WebSiteId))]
        public virtual WebSite WebSite { get; private set; } = default!;

        // ---------- Translations ----------
        public virtual ICollection<ProductCategoryTranslation> Translations { get; private set; } = new List<ProductCategoryTranslation>();

        // ---------- Products ----------
        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();
        public virtual ICollection<Product_ProductCategory_Mapping> Product_ProductCategories { get; private set; } = new List<Product_ProductCategory_Mapping>();

        // ---------- Hierarchical (والد/فرزند) ----------
        public long? ParentId { get; private set; }

        [ForeignKey(nameof(ParentId))]
        [JsonIgnore] // حیاتی: جلوگیری از Infinite Loop در JSON
        public virtual ProductCategory? Parent { get; private set; }

        [InverseProperty(nameof(Parent))]
        [JsonIgnore] // حیاتی: جلوگیری از لود شدن بی‌نهایت فرزندان
        public virtual ICollection<ProductCategory> Children { get; private set; } = new List<ProductCategory>();

        // ---------- Media ----------
        public virtual ICollection<MediaAttachment> MediaAttachments { get; private set; } = new List<MediaAttachment>();

        [NotMapped]
        public MediaAttachment? MainImage => MediaAttachments.FirstOrDefault(m => m.Purpose == "Main");

        [NotMapped]
        public IEnumerable<MediaAttachment> GalleryImages => MediaAttachments
            .Where(m => m.Purpose == "Gallery")
            .OrderBy(m => m.DisplayOrder);

        // ---------- Constructors ----------
        protected ProductCategory() { } // EF Core

        private ProductCategory(long webSiteId)
        {
            WebSiteId = webSiteId > 0 ? webSiteId : throw new DomainException("Invalid WebSiteId.");
        }

        public static ProductCategory Create(long webSiteId) => new(webSiteId);

        // ---------- Parent/Child Methods (بهبود یافته) ----------
        public void SetParent(ProductCategory? parent)
        {
            if (parent != null)
            {
                // 1. خودش نباشه
                if (parent.Id == Id)
                    throw new DomainException("A category cannot be its own parent.");

                // 2. متعلق به همان وبسایت باشه
                if (parent.WebSiteId != WebSiteId)
                    throw new DomainException("Parent category must belong to the same website.");

                // 3. حلقه ایجاد نکنه (Cycle Detection)
                var ancestor = parent.Parent;
                while (ancestor != null)
                {
                    if (ancestor.Id == Id)
                        throw new DomainException("Circular reference detected: cannot set parent.");
                    ancestor = ancestor.Parent;
                }
            }

            Parent = parent;
            ParentId = parent?.Id;
        }

        public void AddChild(ProductCategory child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (child.Id == Id) throw new DomainException("A category cannot add itself as a child.");
            if (child.WebSiteId != WebSiteId) throw new DomainException("Child must belong to the same website.");

            // جلوگیری از دوبار اضافه شدن
            if (Children.Any(c => c.Id == child.Id)) return;

            child.SetParent(this);
            Children.Add(child);
        }

        public void RemoveChild(ProductCategory child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (Children.Remove(child))
            {
                child.ClearParent();
            }
        }

        public void ClearParent()
        {
            Parent = null;
            ParentId = null;
        }

        // ---------- Helper Methods (خیلی کاربردی!) ----------
        public IEnumerable<ProductCategory> GetAllDescendants()
        {
            var result = new List<ProductCategory>();
            foreach (var child in Children.ToList())
            {
                result.Add(child);
                result.AddRange(child.GetAllDescendants());
            }
            return result;
        }

        public IEnumerable<ProductCategory> GetAllAncestors()
        {
            var ancestors = new List<ProductCategory>();
            var current = Parent;
            while (current != null)
            {
                ancestors.Add(current);
                current = current.Parent;
            }
            return ancestors;
        }

        public string GetFullPath(string separator = " > ")
        {
            var parts = GetAllAncestors().Reverse().Select(c => c.GetTitle()).ToList();
            parts.Add(GetTitle());
            return string.Join(separator, parts);
        }

        public string GetTitle(long? languageId = null)
        {
            var translation = languageId.HasValue
                ? Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId)
                : Translations.FirstOrDefault();

            return translation?.Title ?? $"Category_{Id}";
        }

        public int GetDepth()
        {
            var depth = 0;
            var current = Parent;
            while (current != null)
            {
                depth++;
                current = current.Parent;
            }
            return depth;
        }

        // ---------- Translation (بهبود کوچک) ----------
        public void AddTranslation(long languageId, string title, string? description, string slug,
            string? metaTitle = null, string? metaDescription = null, string? metaKeywords = null, string? canonicalUrl = null)
        {
            if (Translations.Any(t => t.WebSiteLanguageId == languageId))
                throw new DomainException($"Translation for language {languageId} already exists.");

            var translation = ProductCategoryTranslation.Create(
                productCategoryId: Id,
                webSiteLanguageId: languageId,
                title: title,
                slug: slug,
                description: description,
                metaTitle: metaTitle,
                metaDescription: metaDescription,
                metaKeywords: metaKeywords,
                canonicalUrl: canonicalUrl
            );

            translation.ProductCategory = this;
            Translations.Add(translation);
        }

        // ---------- Translation - AddOrUpdate ----------
        public void AddOrUpdateTranslation(
            long languageId,
            string title,
            string? description,
            string slug,
            string? metaTitle = null,
            string? metaDescription = null,
            string? metaKeywords = null,
            string? canonicalUrl = null)
        {
            var existing = Translations.FirstOrDefault(t => t.WebSiteLanguageId == languageId);

            if (existing == null)
            {
                // اگر وجود نداشت → اضافه کن
                AddTranslation(
                    languageId: languageId,
                    title: title,
                    description: description,
                    slug: slug,
                    metaTitle: metaTitle,
                    metaDescription: metaDescription,
                    metaKeywords: metaKeywords,
                    canonicalUrl: canonicalUrl
                );
            }
            else
            {
                // اگر وجود داشت → آپدیت کن
                existing.Update(
                    title: title,
                    description: description,
                    slug: slug
                );

                existing.UpdateMeta(
                    metaTitle: metaTitle,
                    metaDescription: metaDescription,
                    metaKeywords: metaKeywords,
                    canonicalUrl: canonicalUrl
                );
            }
        }

        // ---------- Media (بدون تغییر — چون کار می‌کنه!) ----------
        public void AddMainImage(MediaAttachment attachment)
        {
            ValidateAttachment(attachment);

            if (MediaAttachments.Any(m => m.Purpose == "Main"))
                throw new DomainException("This category already has a main image.");

            attachment.AttachTo(Id, nameof(ProductCategory), "Main");
            MediaAttachments.Add(attachment);
        }

        public void AddGalleryImage(MediaAttachment attachment)
        {
            ValidateAttachment(attachment);

            var nextOrder = MediaAttachments.Count(m => m.Purpose == "Gallery") + 1;
            attachment.AttachTo(Id, nameof(ProductCategory), "Gallery", nextOrder);
            MediaAttachments.Add(attachment);
        }

        public void RemoveImage(long mediaAttachmentId)
        {
            var image = MediaAttachments.FirstOrDefault(m => m.Id == mediaAttachmentId)
                        ?? throw new DomainException("Image not found.");
            MediaAttachments.Remove(image);
        }

        private static void ValidateAttachment(MediaAttachment attachment)
        {
            if (attachment == null) throw new DomainException("Attachment cannot be null.");
            if (attachment.MediaFileId <= 0) throw new DomainException("Invalid MediaFile.");
        }
    }
}