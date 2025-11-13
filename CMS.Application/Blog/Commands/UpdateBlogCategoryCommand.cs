using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Blog.Commands
{
    public class UpdateBlogCategoryCommand: IAppRequest<ResultModel>
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }

        public long WebSiteLanguageId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
    }
}
