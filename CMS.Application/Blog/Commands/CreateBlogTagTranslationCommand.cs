using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Blog.Commands
{
    public class CreateBlogTagTranslationCommand : IAppRequest<ResultModel<long>>
    {
        public long BlogTagId { get; set; }
        public long WebSiteLanguageId { get; set; }

        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
    }
}
