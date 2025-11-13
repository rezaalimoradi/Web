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
    public class DeleteProductAttributeTranslationCommandHandler
        : IAppRequestHandler<DeleteProductAttributeTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteProductAttributeTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(DeleteProductAttributeTranslationCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetRepository<Product>()
                .GetAsync(
                    predicate: p => p.Id == request.ProductId && p.WebSiteId == _tenantContext.TenantId,
                    include: p => p.Include(x => x.ProductProductAttributes)
                );

            if (product == null)
                return ResultModel.Fail("Product not found.");

            var attribute = product.ProductProductAttributes.FirstOrDefault(a => a.Id == request.AttributeId);
            if (attribute == null)
                return ResultModel.Fail("Attribute not found.");

            try
            {
                attribute.Product.RemoveTranslation(request.LanguageId);

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
