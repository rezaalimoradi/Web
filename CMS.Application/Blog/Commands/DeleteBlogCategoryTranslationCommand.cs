using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Blog.Commands
{
    public class DeleteBlogCategoryTranslationCommand : IAppRequest<ResultModel>
    {
        public long Id { get; set; }
        public long BlogCategoryId { get; set; }
    }
}
