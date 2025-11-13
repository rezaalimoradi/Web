using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    internal class DeleteProductCategoryTranslationCommandHandler : IAppRequestHandler<DeleteProductCategoryTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteProductCategoryTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(DeleteProductCategoryTranslationCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ProductCategoryTranslation>();

            var translation = await repository.GetAsync(
                predicate: x => x.Id == request.Id && x.ProductCategory.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(t => t.ProductCategory));

            if (translation == null)
                return ResultModel.Fail("Product category translation not found.");

            try
            {
                repository.Delete(translation);
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
