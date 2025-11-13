using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;

namespace CMS.Application.Blog.Queries
{
    public class GetBlogTagsPagedQuery : IAppRequest<ResultModel<IPagedList<BlogTag>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
