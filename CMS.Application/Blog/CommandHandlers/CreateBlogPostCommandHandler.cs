using CMS.Application.Blog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Application.Blog.CommandHandlers
{
    public class CreateBlogPostCommandHandler : IAppRequestHandler<CreateBlogPostCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateBlogPostCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blogPost = new BlogPost(
                    websiteId: _tenantContext.TenantId,
                    status: request.Status,
                    readingTime: request.ReadingTime,
                    publishAt: request.PublishAt,
                    allowComment: request.AllowComment
                );

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

                blogPost.SetCategories(request.CategoryIds);
                blogPost.SetTags(request.TagIds);

                await _unitOfWork.GetRepository<BlogPost>().InsertAsync(blogPost);
                await _unitOfWork.SaveChangesAsync();

                return ResultModel<long>.Success(blogPost.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
