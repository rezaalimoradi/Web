using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Navigation.Entities
{
    public class MenuItemTranslation : BaseEntity
    {
        public string Title { get; set; }
        public string Link { get; set; }

        public long MenuItemId { get; set; }
        public long WebSiteLanguageId { get; set; }

        public MenuItem MenuItem { get; set; }
        public WebSiteLanguage WebSiteLanguage { get; set; }

        private MenuItemTranslation() { }

        public MenuItemTranslation(string title, string link, long menuItemId, long webSiteLanguageId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(link))
                throw new DomainException("Link cannot be null or whitespace.");

            Title = title;
            Link = link;
            MenuItemId = menuItemId;
            WebSiteLanguageId = webSiteLanguageId;
        }

        public void Update(string title, string link)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(link))
                throw new DomainException("Link cannot be null or whitespace.");

            Title = title;
            Link = link;
        }
    }
}
