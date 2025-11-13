using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Pages.Entities
{
    public class PageTranslation : BaseEntity
    {
        public long PageId { get; private set; }
        public Page Page { get; private set; }

        public long WebSiteLanguageId { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Content { get; private set; }

        public string SeoTitle { get; private set; }
        public string SeoDescription { get; private set; }
        public string MetaKeywords { get; private set; }
        public string CanonicalUrl { get; private set; }

        protected PageTranslation() { }

        public PageTranslation(
            long pageId,
            long languageId,
            string title,
            string slug,
            string content,
            string seoTitle,
            string seoDescription,
            string metaKeywords,
            string canonicalUrl)
        {
            PageId = pageId;
            WebSiteLanguageId = languageId;
            Title = title;
            Slug = slug;
            Content = content;
            SeoTitle = seoTitle;
            SeoDescription = seoDescription;
            MetaKeywords = metaKeywords;
            CanonicalUrl = canonicalUrl;
        }

        public void Update(
        string title,
        string slug,
        string content,
        string seoTitle,
        string seoDescription,
        string metaKeywords,
        string canonicalUrl)
        {
            Title = title;
            Slug = slug;
            Content = content;
            SeoTitle = seoTitle;
            SeoDescription = seoDescription;
            MetaKeywords = metaKeywords;
            CanonicalUrl = canonicalUrl;
        }
    }
}
