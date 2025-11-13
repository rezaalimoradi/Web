using CMS.Application.Blog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.CommandHandlers
{
    public class UpdateBlogPostTranslationCommandHandler : IAppRequestHandler<UpdateBlogPostTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateBlogPostTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(UpdateBlogPostTranslationCommand request, CancellationToken cancellationToken)
        {
            var blogPost = await _unitOfWork.GetRepository<BlogPost>().GetAsync(
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
                translation.Update(request.Title, request.Content, request.Slug, request.Summary, request.SeoTitle, request.SeoDescription, request.MetaKeywords, request.CanonicalUrl);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel.Success();
            }
            catch (Exception ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
