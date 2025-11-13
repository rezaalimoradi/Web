using CMS.Application.Blog.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Blog.Entities;

namespace CMS.Application.Blog.Queries
{
    public class GetBlogCategoriesByParentIdQuery : IAppRequest<ResultModel<List<BlogCategory>>>
    {
        public long? ParentId { get; set; }
    }
}
