using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Blog.Entities;

namespace CMS.Application.Blog.Queries
{
    public class GetBlogPostByIdQuery : IAppRequest<ResultModel<BlogPost?>>
    {
        public long Id { get; set; }
    }
}
