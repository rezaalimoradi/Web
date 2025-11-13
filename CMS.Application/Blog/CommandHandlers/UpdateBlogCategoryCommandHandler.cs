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
    public class UpdateBlogCategoryCommandHandler : IAppRequestHandler<UpdateBlogCategoryCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateBlogCategoryCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(UpdateBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<BlogCategory>();

            var category = await repository.GetAsync(
                                        predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                                        include: x => x.Include(y => y.Translations));
            if (category == null)
            {
                return ResultModel.Fail("BlogCategory not found.");
            }

            try
            {
                if (request.ParentId != category.ParentId)
                {
                    if (request.ParentId.HasValue)
                    {
                        var parentCategory = await repository.GetAsync(
                                                    predicate: x => x.Id == request.ParentId.Value && x.WebSiteId == _tenantContext.TenantId);
                        if (parentCategory == null)
                        {
                            return ResultModel.Fail("Parent BlogCategory not found.");
                        }

                        category.SetParent(parentCategory);
                    }
                    else
                    {
                        category.SetParent(null);
                    }
                }

                category.SetUpdatedAt();

                var translation = category.Translations.FirstOrDefault(x => x.WebSiteLanguageId == request.WebSiteLanguageId);
                if (translation == null)
                {
                    return ResultModel.Fail("BlogCategory translation not found.");
                }

                translation.Update(request.Title, request.Slug, request.Description);

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
