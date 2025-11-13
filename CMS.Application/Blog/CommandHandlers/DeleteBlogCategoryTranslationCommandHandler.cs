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
    public class DeleteBlogCategoryTranslationCommandHandler : IAppRequestHandler<DeleteBlogCategoryTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteBlogCategoryTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(DeleteBlogCategoryTranslationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<BlogCategory>();

            var blogCategory = await repo.GetAsync(
                                                predicate: x => x.Id == request.BlogCategoryId && x.WebSiteId == _tenantContext.TenantId,
                                                include: x => x.Include(y => y.Translations));
            if (blogCategory == null)
            {
                return ResultModel.Fail("BlogCategory not found.");
            }

            var translation = blogCategory.Translations.FirstOrDefault(x => x.Id == request.Id);
            if (translation == null)
            {
                return ResultModel.Fail("BlogCategoryTranslation not found.");
            }
            try
            {
                blogCategory.RemoveTranslation(translation);

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
