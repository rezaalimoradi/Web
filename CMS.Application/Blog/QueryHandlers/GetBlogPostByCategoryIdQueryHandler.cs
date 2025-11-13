using CMS.Application.Blog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.QueryHandlers
{
    public class GetBlogPostByCategoryIdQueryHandler
        : IAppRequestHandler<GetBlogPostByCategoryIdQuery, ResultModel<List<BlogPost>?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetBlogPostByCategoryIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<BlogPost>?>> Handle(GetBlogPostByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var posts = await _unitOfWork.GetRepository<BlogPost>()
                .GetAllAsync(
                    predicate: p => p.WebSiteId == _tenantContext.TenantId
                                   && p.Categories.Any(pc => pc.BlogCategoryId == request.CategoryId),
                    func: q => q
                        .Include(p => p.Translations)
                        .Include(p => p.Categories)
                            .ThenInclude(pc => pc.BlogCategory)
                                .ThenInclude(c => c.Translations)
                );

            return ResultModel<List<BlogPost>?>.Success(posts.ToList());
        }
    }
}
