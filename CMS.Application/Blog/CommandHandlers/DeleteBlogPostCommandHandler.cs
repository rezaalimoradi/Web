using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.Commands.Handlers
{
    public class DeleteBlogPostCommandHandler : IAppRequestHandler<DeleteBlogPostCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteBlogPostCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<BlogPost>();

            var blogPost = await repository.Table
                .FirstOrDefaultAsync(p => p.Id == request.Id && p.WebSiteId == _tenantContext.TenantId, cancellationToken);

            if (blogPost == null)
                return ResultModel.Fail("Blog post not found or access denied.");

            try
            {
                repository.Delete(blogPost);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel.Success();
            }
            catch (DomainException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
