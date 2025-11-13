using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Blog.Commands
{
    public class UpdateBlogTagCommand : IAppRequest<ResultModel>
    {
        public long Id { get; set; }
        public long WebSiteLanguageId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
