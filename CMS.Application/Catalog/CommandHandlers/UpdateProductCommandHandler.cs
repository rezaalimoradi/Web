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
    public class UpdateProductCommandHandler : IAppRequestHandler<UpdateProductCommand, ResultModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Product>();

            var product = await repository.GetAsync(
                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(p => p.Translations));

            if (product == null)
            {
                return ResultModel.Fail("Product not found.");
            }

            try
            {

                product.Update(
                    sku: request.SKU,
                    brandId: request.BrandId,
                    manageInventory: request.ManageInventory,
                    type: request.Type,
                    isOriginal: request.IsOriginal,
                    barcode: request.Barcode,
                    width: request.Width,
                    height: request.Height,
                    length: request.Length,
                    isPublished: request.IsPublished,
                    showOnHomepage: request.ShowOnHomepage,
                    allowCustomerReviews: request.AllowCustomerReviews,
                    isCallForPrice: request.IsCallForPrice,
                    taxId: request.TaxId
                );

                var translation = product.Translations
                    .FirstOrDefault(t => t.WebSiteLanguageId == request.WebSiteLanguageId);

                if (translation == null)
                {
                    return ResultModel.Fail("Product translation not found.");
                }

                translation.Update(request.Name, request.Description, request.Slug);

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
