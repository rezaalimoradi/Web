using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Pages.Enums;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Pages.Entities
{
    public class Page : AggregateRoot
    {
        public PageStatus Status { get; private set; }
        public DateTime? PublishAt { get; private set; }
        public bool ShowInMenu { get; private set; }

        public long WebSiteId { get; private set; }
        public WebSite WebSite { get; private set; }

        private readonly List<PageTranslation> _translations = new();
        public IReadOnlyCollection<PageTranslation> Translations => _translations.AsReadOnly();

        protected Page() { }

        public Page(long websiteId, PageStatus status, DateTime? publishAt, bool showInMenu)
        {
            WebSiteId = websiteId;
            Status = status;
            PublishAt = publishAt;
            ShowInMenu = showInMenu;
        }

        public PageTranslation AddTranslation(
            long languageId,
            string title,
            string slug,
            string content,
            string seoTitle,
            string seoDescription,
            string metaKeywords,
            string canonicalUrl)
        {
            if (_translations.Any(x => x.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            var translation = new PageTranslation(
                pageId: this.Id,
                languageId: languageId,
                title: title,
                slug: slug,
                content: content,
                seoTitle: seoTitle,
                seoDescription: seoDescription,
                metaKeywords: metaKeywords,
                canonicalUrl: canonicalUrl
            );

            _translations.Add(translation);
            return translation;
        }

        public void RemoveTranslation(PageTranslation translation)
        {
            if (!_translations.Any(x => x.Id == translation.Id))
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }

        public void UpdateStatus(PageStatus status) => Status = status;
        public void UpdatePublishDate(DateTime? publishAt) => PublishAt = publishAt;
        public void UpdateShowInMenu(bool showInMenu) => ShowInMenu = showInMenu;
    }
}
