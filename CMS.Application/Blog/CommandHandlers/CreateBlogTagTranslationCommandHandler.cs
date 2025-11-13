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
    public class CreateBlogTagTranslationCommandHandler : IAppRequestHandler<CreateBlogTagTranslationCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateBlogTagTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateBlogTagTranslationCommand request, CancellationToken cancellationToken)
        {
            var blogTag = await _unitOfWork.GetRepository<BlogTag>().GetAsync(
                predicate: x => x.Id == request.BlogTagId && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(y => y.Translations));
            if (blogTag == null)
            {
                return ResultModel<long>.Fail("BlogTag not found.");
            }

            try
            {
                var blogTagTranslation = blogTag.AddTranslation(
                    languageId: request.WebSiteLanguageId,
                    name: request.Name,
                    slug: request.Slug);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<long>.Success(blogTagTranslation.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
