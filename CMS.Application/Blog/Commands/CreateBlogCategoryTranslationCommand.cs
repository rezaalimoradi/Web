using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Blog.Commands
{
    public class CreateBlogCategoryTranslationCommand : IAppRequest<ResultModel<long>>
    {
        public long BlogCategoryId { get; private set; }
        public long WebSiteLanguageId { get; private set; }

        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
    }
}
