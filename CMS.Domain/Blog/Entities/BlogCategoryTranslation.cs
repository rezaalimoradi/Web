using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Blog.Entities
{
    public class BlogCategoryTranslation : BaseEntity
    {
        protected BlogCategoryTranslation() { }

        public long BlogCategoryId { get; private set; }
        public long WebSiteLanguageId { get; private set; }

        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }

        public BlogCategory BlogCategory { get; private set; }
        public WebSiteLanguage WebSiteLanguage { get; private set; }

        internal BlogCategoryTranslation(long blogCategoryId, long languageId, string title, string slug, string description)
        {
            BlogCategoryId = blogCategoryId;
            WebSiteLanguageId = languageId;
            Title = title;
            Slug = slug;
            Description = description;
        }

        public void Update(string title, string slug, string description)
        {
            Title = title?.Trim() ?? throw new ArgumentNullException(nameof(title));
            Slug = slug?.Trim() ?? throw new ArgumentNullException(nameof(slug));
            Description = description?.Trim() ?? throw new ArgumentNullException(nameof(description));
            Description = description?.Trim();
        }
    }
}
