using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;
using CMS.Infrastructure.Persistence.Configurations.Blog;

namespace CMS.Domain.Blog.Entities
{
    public class BlogTag : AggregateRoot
    {
        private readonly List<BlogTagTranslation> _translations = new();
        private readonly List<BlogPostTag> _blogPostTags = new();

        public long WebSiteId { get; private set; }
        public WebSite WebSite { get; private set; }

        public IReadOnlyCollection<BlogTagTranslation> Translations => _translations.AsReadOnly();
        public IReadOnlyCollection<BlogPostTag> BlogPostTags => _blogPostTags.AsReadOnly();

        private BlogTag() { }

        public BlogTag(long webSiteId)
        {
            WebSiteId = webSiteId;
        }

        public BlogTagTranslation AddTranslation(long languageId, string name, string slug)
        {
            if (_translations.Any(x => x.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");


            var blogTagTranslation = new BlogTagTranslation(
                blogTagId: this.Id,
                languageId: languageId,
                name: name,
                slug: slug);

            _translations.Add(blogTagTranslation);

            return blogTagTranslation;
        }

        public void UpdateTranslation(long languageId, string name, string slug)
        {
            var translation = _translations.FirstOrDefault(x => x.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation not found for the specified language.");

            translation.Name = name;
            translation.Slug = slug;
        }

        public void RemoveTranslation(BlogTagTranslation translation)
        {
            if (!_translations.Any(x => x.Id == translation.Id))
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }
    }
}
