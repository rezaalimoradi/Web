using CMS.Domain.Blog.Enums;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using CMS.Domain.Localization.Entities;
using CMS.Domain.Tenants.Entitis;
using CMS.Infrastructure.Persistence.Configurations.Blog;

namespace CMS.Domain.Blog.Entities
{
    public class BlogPost : AggregateRoot
    {
        public BlogPostStatus Status { get; private set; }
        public uint ReadingTime { get; private set; }
        public DateTime? PublishAt { get; private set; }
        public bool AllowComment { get; private set; }

        public long WebSiteId { get; private set; }
        public WebSite WebSite { get; private set; }

        private readonly List<BlogPostTranslation> _translations = new();
        public IReadOnlyCollection<BlogPostTranslation> Translations => _translations.AsReadOnly();

        private readonly List<BlogPostCategory> _categories = new();
        public IReadOnlyCollection<BlogPostCategory> Categories => _categories.AsReadOnly();

        private readonly List<BlogPostTag> _tags = new();
        public IReadOnlyCollection<BlogPostTag> Tags => _tags.AsReadOnly();
        protected BlogPost() { }

        public BlogPost(long websiteId, BlogPostStatus status, uint readingTime, DateTime? publishAt, bool allowComment)
        {
            WebSiteId = websiteId;
            Status = status;
            ReadingTime = readingTime;
            PublishAt = publishAt;
            AllowComment = allowComment;
        }

        public BlogPostTranslation AddTranslation(long languageId, string title, string slug, string summary, string content, string seoTitle, string seoDescription, string metaKeywords, string canonicalUrl)
        {
            if (_translations.Any(x => x.WebSiteLanguageId == languageId))
                throw new DomainException("Translation for this language already exists.");

            var blogPostTranslation = new BlogPostTranslation(
                blogPostId: this.Id,
                languageId: languageId,
                title: title,
                slug: slug,
                summary: summary,
                content: content,
                seoTitle: seoTitle,
                seoDescription: seoDescription,
                metaKeywords: metaKeywords,
                canonicalUrl: canonicalUrl);

            _translations.Add(blogPostTranslation);

            return blogPostTranslation;
        }

        public void RemoveTranslation(BlogPostTranslation translation)
        {
            if (!_translations.Any(x=> x.Id == translation.Id))
                throw new DomainException("Translation not found.");

            _translations.Remove(translation);
        }

        public void SetCategories(IEnumerable<long> categoryIds)
        {
            _categories.Clear();
            foreach (var id in categoryIds.Distinct())
            {
                _categories.Add(new BlogPostCategory
                {
                    BlogPostId = this.Id,
                    BlogCategoryId = id
                });
            }
        }

        public void SetTags(IEnumerable<long> tagIds)
        {
            _tags.Clear();
            foreach (var id in tagIds.Distinct())
            {
                _tags.Add(new BlogPostTag
                {
                    BlogPostId = this.Id,
                    BlogTagId = id
                });
            }
        }

        public void UpdateStatus(BlogPostStatus status) => Status = status;
        public void UpdatePublishDate(DateTime? publishAt) => PublishAt = publishAt;
        public void UpdateReadingTime(uint readingTime) => ReadingTime = readingTime;
        public void UpdateAllowComment(bool allowComment) => AllowComment = allowComment;
    }
}
