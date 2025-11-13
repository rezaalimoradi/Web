using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Blog.Commands
{
    public class UpdateBlogCategoryTranslationCommand : IAppRequest<ResultModel>
    {
        public long BlogCategoryId { get; private set; }
        public long WebSiteLanguageId { get; private set; }

        public string Title { get; private set; } = null!;
        public string Slug { get; private set; } = null!;
        public string Description { get; private set; } = null!;
    }
}
