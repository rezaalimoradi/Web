using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Application.Catalog.CommandHandlers
{
    internal class DeleteProductCategoryCommandHandler
        : IAppRequestHandler<DeleteProductCategoryCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteProductCategoryCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ProductCategory>();

            var category = await repository.GetAsync(
                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                include: x => x // می‌توان Include برای فرزندان یا رسانه‌ها اضافه کرد در آینده
                                //.Include(c => c.Children)
                                //.Include(c => c.MediaAttachments)
            );

            if (category is null)
                return ResultModel.Fail("Product category not found.");

            try
            {
                // اگر نیاز به بررسی فرزندان یا وابستگی‌ها باشد، قبل از حذف بررسی می‌کنیم
                if (category.Children.Any())
                    return ResultModel.Fail("Cannot delete a category that has child categories.");

                repository.Delete(category);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel.Success();
            }
            catch (DomainException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return ResultModel.Fail("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
