using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Domain.Blog.Entities
{
    public class BlogCategory : AggregateRoot
    {
        private readonly List<BlogCategoryTranslation> _translations = new();
        private readonly List<BlogPostCategory> _blogPostCategories = new();

        protected BlogCategory() { }

        public BlogCategory(long webSiteId)
        {
            WebSiteId = webSiteId;
        }

        public long WebSiteId { get; private set; }
        public WebSite WebSite { get; private set; }

        public long? ParentId { get; set; }
        public BlogCategory? Parent { get; set; }
        public ICollection<BlogCategory> Children { get; set; } = [];

        public IReadOnlyCollection<BlogCategoryTranslation> Translations => _translations.AsReadOnly();
        public IReadOnlyCollection<BlogPostCategory> BlogPostCategories => _blogPostCategories.AsReadOnly();

        #region Domain Behaviors

        public BlogCategoryTranslation AddTranslation(long languageId, string title, string slug, string description)
        {
            if (_translations.Any(x => x.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            var translation = new BlogCategoryTranslation(Id, languageId, title, slug, description);
            _translations.Add(translation);

            return translation;
        }


        public void UpdateTranslation(long languageId, string title, string slug, string description)
        {
            var translation = _translations.FirstOrDefault(x => x.WebSiteLanguageId == languageId);
            if (translation == null)
                throw new DomainException("Translation for this language does not exist.");

            translation.Update(title, slug, description);
        }

        public void RemoveTranslation(BlogCategoryTranslation translation)
        {
            if (!_translations.Any(x => x.Id == translation.Id))
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }

        public void SetParent(BlogCategory? parent)
        {
            Parent = parent;
            ParentId = parent?.Id;
        }
        #endregion
    }
}
