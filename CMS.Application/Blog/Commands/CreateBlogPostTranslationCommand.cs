using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Blog.Commands
{
    public class CreateBlogPostTranslationCommand : IAppRequest<ResultModel<long>>
    {
        public long BlogPostId { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string SeoTitle { get; set; } = null!;
        public string SeoDescription { get; set; } = null!;
        public string MetaKeywords { get; set; } = null!;
        public string CanonicalUrl { get; set; } = null!;
        public long WebSiteLanguageId { get; set; }
    }
}
