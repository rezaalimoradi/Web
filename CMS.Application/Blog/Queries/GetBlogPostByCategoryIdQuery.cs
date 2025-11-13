using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Blog.Entities;

namespace CMS.Application.Blog.Queries
{
    public class GetBlogPostByCategoryIdQuery : IAppRequest<ResultModel<List<BlogPost>?>>
    {
        public long CategoryId { get; set; }
    }
}
