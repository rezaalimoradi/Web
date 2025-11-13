using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.Commands.Handlers
{
    public class UpdateBlogPostCommandHandler : IAppRequestHandler<UpdateBlogPostCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateBlogPostCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<BlogPost>();

                var blogPost = await repository.Table
                    .Include(p => p.Translations)
                    .Include(p => p.Categories)
                    .Include(p => p.Tags)
                    .FirstOrDefaultAsync(p => p.Id == request.Id && p.WebSiteId == _tenantContext.TenantId, cancellationToken);

                if (blogPost == null)
                    return ResultModel<long>.Fail("Blog post not found or access denied.");

                blogPost.UpdateStatus(request.Status);
                blogPost.UpdateReadingTime(request.ReadingTime);
                blogPost.UpdatePublishDate(request.PublishAt);
                blogPost.UpdateAllowComment(request.AllowComment);

                var existingTranslation = blogPost.Translations
                    .FirstOrDefault(t => t.WebSiteLanguageId == request.WebSiteLanguageId);

                if (existingTranslation != null)
                {
                    existingTranslation.Update(
                        title: request.Title,
                        slug: request.Slug,
                        summary: request.Summary,
                        content: request.Content,
                        seoTitle: request.SeoTitle,
                        seoDescription: request.SeoDescription,
                        metaKeywords: request.MetaKeywords,
                        canonicalUrl: request.CanonicalUrl);
                }
                else
                {
                    blogPost.AddTranslation(
                        request.WebSiteLanguageId,
                        request.Title,
                        request.Slug,
                        request.Summary,
                        request.Content,
                        request.SeoTitle,
                        request.SeoDescription,
                        request.MetaKeywords,
                        request.CanonicalUrl
                    );
                }

                blogPost.SetCategories(request.CategoryIds);
                blogPost.SetTags(request.TagIds);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(blogPost.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
