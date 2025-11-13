using CMS.Application.Blog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.CommandHandlers
{
    public class UpdateBlogTagTranslationCommandHandler : IAppRequestHandler<UpdateBlogTagTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateBlogTagTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(UpdateBlogTagTranslationCommand request, CancellationToken cancellationToken)
        {
            var blogTag = await _unitOfWork.GetRepository<BlogTag>().GetAsync(
                                                predicate: x => x.Id == request.BlogTagId && x.WebSiteId == _tenantContext.TenantId,
                                                include: x => x.Include(y => y.Translations));
            if (blogTag == null)
            {
                return ResultModel.Fail("BlogTag not found.");
            }

            var translation = blogTag.Translations.FirstOrDefault(x => x.Id == request.BlogTagId);
            if (translation == null)
            {
                return ResultModel.Fail("BlogTagTranslation not found.");
            }

            try
            {
                translation.Update(request.Name, request.Slug,request.WebSiteLanguageId);

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
