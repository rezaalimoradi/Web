using CMS.Application.Blog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Application.Blog.CommandHandlers
{
    public class CreateBlogCategoryCommandHandler : IAppRequestHandler<CreateBlogCategoryCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateBlogCategoryCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new BlogCategory(_tenantContext.TenantId)
            {
                ParentId = request.ParentId
            };

            try
            {
                category.AddTranslation(request.WebSiteLanguageId, request.Title, request.Slug, request.Description);

                await _unitOfWork.GetRepository<BlogCategory>().InsertAsync(category);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResultModel<long>.Success(category.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
        }
    }
}
