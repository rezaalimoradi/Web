using CMS.Application.Blog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.CommandHandlers
{
    public class UpdateBlogCategoryTranslationCommandHandler : IAppRequestHandler<UpdateBlogCategoryTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateBlogCategoryTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(UpdateBlogCategoryTranslationCommand request, CancellationToken cancellationToken)
        {
            var blogTag = await _unitOfWork.GetRepository<BlogCategory>().GetAsync(
                                                predicate: x => x.Id == request.BlogCategoryId && x.WebSiteId == _tenantContext.TenantId,
                                                include: x => x.Include(y => y.Translations));
            if (blogTag == null)
            {
                return ResultModel.Fail("BlogCategory not found.");
            }

            var translation = blogTag.Translations.FirstOrDefault(x => x.Id == request.BlogCategoryId);
            if (translation == null)
            {
                return ResultModel.Fail("BlogCategoryTranslation not found.");
            }

            try
            {
                translation.Update(request.Title, request.Slug,request.Description);

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
