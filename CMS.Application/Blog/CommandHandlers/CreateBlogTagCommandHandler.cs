using CMS.Application.Blog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Application.Blog.CommandHandlers
{
    public class CreateBlogTagCommandHandler : IAppRequestHandler<CreateBlogTagCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateBlogTagCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateBlogTagCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blogTag = new BlogTag(_tenantContext.TenantId);

                blogTag.AddTranslation(request.WebSiteLanguageId, request.Name, request.Slug);

                await _unitOfWork.GetRepository<BlogTag>().InsertAsync(blogTag);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(blogTag.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
