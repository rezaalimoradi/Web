using CMS.Application.Blog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.QueryHandlers
{
    public class GetBlogTagsPagedQueryHandler : IAppRequestHandler<GetBlogTagsPagedQuery, ResultModel<IPagedList<BlogTag>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetBlogTagsPagedQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<IPagedList<BlogTag>>> Handle(GetBlogTagsPagedQuery request, CancellationToken cancellationToken)
        {
            var blogTags = await _unitOfWork.GetRepository<BlogTag>().GetAllPagedAsync(
                                                predicate: x => x.WebSiteId == _tenantContext.TenantId,
                                                func: x => x.Include(y => y.Translations),
                                                pageIndex: request.Page,
                                                pageSize: request.PageSize);

            return ResultModel<IPagedList<BlogTag>>.Success(blogTags);
        }
    }
}
