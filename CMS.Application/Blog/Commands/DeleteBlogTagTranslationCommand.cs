using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Blog.Commands
{
    public class DeleteBlogTagTranslationCommand : IAppRequest<ResultModel>
    {
        public long Id { get; set; }
        public long BlogTagId { get; set; }
    }
}
