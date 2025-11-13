using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Blog.Entities;

namespace CMS.Application.Blog.Queries
{
    public class GetBlogCategoryByIdQuery : IAppRequest<ResultModel<BlogCategory?>>
    {
        public long Id { get; set; }
    }
}
