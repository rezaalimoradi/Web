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
    public class UpdateProductAttributeValueCommandHandler
        : IAppRequestHandler<UpdateProductAttributeValueCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateProductAttributeValueCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<bool>> Handle(UpdateProductAttributeValueCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetRepository<Product>()
                .GetAsync(
                    predicate: p => p.Id == request.ProductId
                                 && p.WebSiteId == _tenantContext.TenantId,
                    include: p => p
                        .Include(p => p.ProductProductAttributes)
                            .ThenInclude(pa => pa.ProductAttribute)
                                .ThenInclude(a => a.Values)
                                    .ThenInclude(v => v.Translations)
                );

            if (product == null)
                return ResultModel<bool>.Fail("Product not found.");

            var attributeLink = product.ProductProductAttributes
                .FirstOrDefault(pa => pa.ProductAttributeId == request.ProductAttributeId);

            if (attributeLink == null)
                return ResultModel<bool>.Fail("Attribute not found.");

            var attribute = attributeLink.ProductAttribute;

            // پیدا کردن مقدار مورد نظر
            var value = attribute.Values.FirstOrDefault(v => v.Id == request.ProductAttributeId);
            if (value == null)
                return ResultModel<bool>.Fail("Attribute value not found.");

            try
            {
                var translation = value.Translations.FirstOrDefault(t => t.WebSiteLanguageId == request.WebSiteLanguageId);

                if (translation == null)
                    value.AddTranslation(request.WebSiteLanguageId, request.Value);
                else
                    value.UpdateTranslation(request.WebSiteLanguageId, request.Value);

                _unitOfWork.GetRepository<Product>().Update(product);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<bool>.Success(true);
            }
            catch (DomainException ex)
            {
                return ResultModel<bool>.Fail(ex.Message);
            }
        }
    }
}
