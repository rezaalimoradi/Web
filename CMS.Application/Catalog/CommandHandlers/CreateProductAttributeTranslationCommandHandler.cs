using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    public class CreateProductAttributeTranslationCommandHandler
        : IAppRequestHandler<CreateProductAttributeTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateProductAttributeTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(CreateProductAttributeTranslationCommand request, CancellationToken cancellationToken)
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
                return ResultModel.Fail("Attribute not found for this product.");

            var attribute = attributeLink.ProductAttribute;

            if (attribute.Translations.Any(t => t.WebSiteLanguageId == request.LanguageId))
                return ResultModel.Fail("Translation for this language already exists.");

            attribute.AddTranslation(request.LanguageId, request.Name);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel.Success();
        }
    }
}
