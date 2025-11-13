using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    public class UpdateProductAttributeTranslationCommandHandler
        : IAppRequestHandler<UpdateProductAttributeTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateProductAttributeTranslationCommandHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(UpdateProductAttributeTranslationCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetRepository<Product>()
                .GetAsync(
                    predicate: p => p.Id == request.ProductId
                                 && p.WebSiteId == _tenantContext.TenantId,
                    include: p => p
                        .Include(p => p.ProductProductAttributes)
                            .ThenInclude(pa => pa.ProductAttribute)
                                .ThenInclude(a => a.Translations)
                );

            if (product == null)
                return ResultModel.Fail("Product not found.");

            var attributeLink = product.ProductProductAttributes
                .FirstOrDefault(pa => pa.ProductAttributeId == request.AttributeId);

            if (attributeLink == null)
                return ResultModel.Fail("Product attribute link not found.");

            var attribute = attributeLink.ProductAttribute;
            if (attribute == null)
                return ResultModel.Fail("Attribute not found.");

            var translation = attribute.Translations
                .FirstOrDefault(t => t.WebSiteLanguageId == request.LanguageId);

            if (translation == null)
                return ResultModel.Fail("Attribute translation not found.");

            translation.Update(request.Name);

            _unitOfWork.GetRepository<Product>().Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel.Success();
        }
    }
}
