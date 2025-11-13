using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Blog.Enums;

namespace CMS.Application.Blog.Commands
{
    public class UpdateBlogPostCommand : IAppRequest<ResultModel<long>>
    {
        public long Id { get; set; }

        public BlogPostStatus Status { get; set; }
        public uint ReadingTime { get; set; }
        public DateTime? PublishAt { get; set; }
        public bool AllowComment { get; set; }

        public long WebSiteLanguageId { get; set; }

        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string SeoTitle { get; set; } = null!;
        public string SeoDescription { get; set; } = null!;
        public string MetaKeywords { get; set; } = null!;
        public string CanonicalUrl { get; set; } = null!;

        public List<long> CategoryIds { get; set; } = new();
        public List<long> TagIds { get; set; } = new();
    }
}
