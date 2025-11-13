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
    public class CreateBlogCategoryTranslationCommandHandler : IAppRequestHandler<CreateBlogCategoryTranslationCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateBlogCategoryTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateBlogCategoryTranslationCommand request, CancellationToken cancellationToken)
        {
            var blogCategory = await _unitOfWork.GetRepository<BlogCategory>().GetAsync(
                predicate: x => x.Id == request.BlogCategoryId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(y => y.Translations));
            if (blogCategory == null)
            {
                return ResultModel<long>.Fail("BlogCategory not found.");
            }

            try
            {
                var blogCategoryTranslation = blogCategory.AddTranslation(
                    languageId: request.WebSiteLanguageId,
                    title: request.Title,
                    description: request.Description,
                    slug: request.Slug);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(blogCategoryTranslation.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
