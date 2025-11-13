using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Blog.Entities
{
    public class BlogPostTranslation : BaseEntity
    {
        public long BlogPostId { get; private set; }
        public long WebSiteLanguageId { get; private set; }

        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Summary { get; private set; }
        public string Content { get; private set; }

        public string SeoTitle { get; private set; }
        public string SeoDescription { get; private set; }
        public string MetaKeywords { get; private set; }
        public string CanonicalUrl { get; private set; }

        public BlogPost BlogPost { get; set; }
        public WebSiteLanguage WebSiteLanguage { get; set; }

        private BlogPostTranslation() { }

        internal BlogPostTranslation(long blogPostId, long languageId, string title, string content, string slug, string summary, string seoTitle, string seoDescription, string metaKeywords, string canonicalUrl)
        {
            BlogPostId = blogPostId;
            WebSiteLanguageId = languageId;
            Title = title;
            Slug = slug;
            Content = content;
            Summary = summary;
            SeoTitle = seoTitle;
            SeoDescription = seoDescription;
            MetaKeywords = metaKeywords;
            CanonicalUrl = canonicalUrl;
        }

        public void Update(string title, string content, string slug, string summary, string seoTitle, string seoDescription, string metaKeywords, string canonicalUrl)
        {
            Title = title?.Trim() ?? throw new ArgumentNullException(nameof(title));
            Slug = slug?.Trim() ?? throw new ArgumentNullException(nameof(slug));
            Summary = summary;
            SeoDescription = seoDescription.Trim();
            SeoTitle = seoTitle.Trim();
            MetaKeywords = metaKeywords.Trim();
            CanonicalUrl = canonicalUrl.Trim();
            Content = content;
            SetUpdatedAt();
        }
    }
}
