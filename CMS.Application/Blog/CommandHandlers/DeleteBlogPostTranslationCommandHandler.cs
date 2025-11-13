using CMS.Application.Blog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.CommandHandlers
{
    public class DeleteBlogPostTranslationCommandHandler : IAppRequestHandler<DeleteBlogPostTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteBlogPostTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(DeleteBlogPostTranslationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<BlogPost>();

            var blogPost = await repo.GetAsync(
                                                predicate: x => x.Id == request.BlogPostId && x.WebSiteId == _tenantContext.TenantId,
                                                include: x => x.Include(y => y.Translations));
            if (blogPost == null)
            {
                return ResultModel.Fail("BlogPost not found.");
            }

            var translation = blogPost.Translations.FirstOrDefault(x => x.Id == request.Id);
            if (translation == null)
            {
                return ResultModel.Fail("BlogPostTranslation not found.");
            }
            try
            {
                blogPost.RemoveTranslation(translation);

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
