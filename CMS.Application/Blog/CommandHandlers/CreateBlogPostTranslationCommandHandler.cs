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
    public class CreateBlogPostTranslationCommandHandler : IAppRequestHandler<CreateBlogPostTranslationCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateBlogPostTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateBlogPostTranslationCommand request, CancellationToken cancellationToken)
        {
            var blogPost = await _unitOfWork.GetRepository<BlogPost>().GetAsync(
                predicate: x => x.Id == request.BlogPostId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(y => y.Translations));
            if (blogPost == null)
            {
                return ResultModel<long>.Fail("BlogPost not found.");
            }

            try
            {
                var blogPostTranslation = blogPost.AddTranslation(
                    languageId: request.WebSiteLanguageId,
                    title: request.Title,
                    slug: request.Slug,
                    summary: request.Summary,
                    content: request.Content,
                    seoTitle: request.SeoTitle,
                    seoDescription: request.SeoDescription,
                    metaKeywords: request.MetaKeywords,
                    canonicalUrl: request.CanonicalUrl);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(blogPostTranslation.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
