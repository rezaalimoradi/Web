using CMS.Application.Blog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.QueryHandlers
{
    public class GetBlogCategoriesByParentIdQueryHandler : IAppRequestHandler<GetBlogCategoriesByParentIdQuery, ResultModel<List<BlogCategory>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetBlogCategoriesByParentIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<BlogCategory>>> Handle(GetBlogCategoriesByParentIdQuery request, CancellationToken cancellationToken)
        {
            var blogCategories = await _unitOfWork.GetRepository<BlogCategory>().GetAllAsync(
                                            predicate: x => x.ParentId == request.ParentId && x.WebSiteId == _tenantContext.TenantId,
                                            func: x => x.Include(y => y.Translations));

            return ResultModel<List<BlogCategory>>.Success(blogCategories.ToList());
        }
    }
}
