using CMS.Domain.Common;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Blog.Entities
{
    public class BlogTagTranslation : BaseEntity
    {
        public long BlogTagId { get; set; }
        public long WebSiteLanguageId { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }

        public BlogTag BlogTag { get; set; }
        public WebSiteLanguage WebSiteLanguage { get; set; }

        private BlogTagTranslation() { }

        internal BlogTagTranslation(long blogTagId, long languageId, string name, string slug)
        {
            BlogTagId = blogTagId;
            WebSiteLanguageId = languageId;
            Name = name;
            Slug = slug;
        }

        public void Update(string name, string slug,long languageId)
        {
            name = name?.Trim() ?? throw new ArgumentNullException(nameof(name));
            slug = slug?.Trim() ?? throw new ArgumentNullException(nameof(slug));
            SetUpdatedAt();
        }

    }

}
