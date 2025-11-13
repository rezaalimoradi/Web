using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.CommandHandlers
{
    public class CreateProductAttributeValueTranslationCommandHandler : IAppRequestHandler<CreateProductAttributeValueTranslationCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateProductAttributeValueTranslationCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(CreateProductAttributeValueTranslationCommand request, CancellationToken cancellationToken)
        {
            var valueRepo = _unitOfWork.GetRepository<Product>();

            var attributeValue = await valueRepo.GetAsync(
                predicate: v =>
                    v.Id == request.AttributeValueId &&
                    v.ProductProductAttributes.Any(pa =>
                        pa.Product != null &&
                        pa.Product.WebSiteId == _tenantContext.TenantId),
                include: v => v
                    .Include(x => x.Translations)
                    .Include(x => x.ProductProductAttributes)
                        .ThenInclude(pa => pa.Product)
                        .ThenInclude(p => p.Product_ProductCategories) // اختیاری، برای فیلتر بعدی
            );

            if (attributeValue == null)
                return ResultModel.Fail("Attribute value not found.");

            if (attributeValue.Translations.Any(t => t.WebSiteLanguageId == request.LanguageId))
                return ResultModel.Fail("Translation for this language already exists.");

            //attributeValue.AddTranslation(request.LanguageId, request.Name);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel.Success();
        }
    }
}
