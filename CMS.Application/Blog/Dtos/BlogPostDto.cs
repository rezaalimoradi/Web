using CMS.Domain.Blog.Enums;

namespace CMS.Application.Blog.Dtos
{
    public class BlogPostDto
    {
        public long Id { get; set; }
        public BlogPostStatus Status { get; set; }
        public uint ReadingTime { get; set; }
        public DateTime? PublishAt { get; set; }
        public bool AllowComment { get; set; }

        public long WebSiteLanguageId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string CanonicalUrl { get; set; }

        public List<long> CategoryIds { get; set; } = new();
        public List<long> TagIds { get; set; } = new();
    }
}
