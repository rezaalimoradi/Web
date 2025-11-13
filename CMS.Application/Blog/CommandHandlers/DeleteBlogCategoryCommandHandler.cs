using CMS.Application.Blog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.CommandHandlers
{
    public class DeleteBlogCategoryCommandHandler : IAppRequestHandler<DeleteBlogCategoryCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteBlogCategoryCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(DeleteBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<BlogCategory>();

            var category = await repository.GetAsync(
                                        predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                                        include: x => x.Include(y => y.Translations));
            if (category == null)
            {
                return ResultModel.Fail("BlogCategory not found.");
            }

            repository.Delete(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel.Success();
        }
    }
}
