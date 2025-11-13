using CMS.Application.Blog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.QueryHandlers
{
    public class GetBlogPostByIdQueryHandler : IAppRequestHandler<GetBlogPostByIdQuery, ResultModel<BlogPost?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetBlogPostByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<BlogPost?>> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetRepository<BlogPost>().GetAsync(
                                                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                                                include: x => x.Include(y => y.Translations));

        }
    }
}
