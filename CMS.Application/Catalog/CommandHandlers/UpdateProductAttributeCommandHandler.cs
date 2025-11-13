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
    public class UpdateProductAttributeCommandHandler
        : IAppRequestHandler<UpdateProductAttributeCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateProductAttributeCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<bool>> Handle(UpdateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productAttribute = await _unitOfWork.GetRepository<ProductAttribute>()
                    .GetAsync(
                        predicate: p => p.Id == request.AttributeId && p.WebSiteId == _tenantContext.TenantId,
                        include: p => p.Include(a => a.Translations)
                    );

                if (productAttribute == null)
                    return ResultModel<bool>.Fail("Product Attribute not found.");

                var translation = productAttribute.Translations
                    .FirstOrDefault(t => t.WebSiteLanguageId == request.WebSiteLanguageId);

                if (translation == null)
                    productAttribute.AddTranslation(request.WebSiteLanguageId, request.Name);
                else
                    translation.Update(request.Name);

                _unitOfWork.GetRepository<ProductAttribute>().Update(productAttribute);
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
