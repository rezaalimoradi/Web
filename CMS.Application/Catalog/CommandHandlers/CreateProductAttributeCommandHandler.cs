using CMS.Application.Catalog.Commands;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Application.Catalog.CommandHandlers
{
    public class CreateProductAttributeCommandHandler
        : IAppRequestHandler<CreateProductAttributeCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateProductAttributeCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productAttribute = new ProductAttribute(
                    _tenantContext.TenantId,
                    request.WebSiteLanguageId,
                    request.Name,
                    request.AllowMultipleValues
                );

                productAttribute.AddTranslation(
                    request.WebSiteLanguageId,
                    request.Name
                );

                await _unitOfWork.GetRepository<ProductAttribute>()
                    .InsertAsync(productAttribute);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (request.ProductId.HasValue && request.ProductId.Value > 0)
                {
                    var productProductAttribute = new ProductProductAttribute(
                        request.ProductId.Value,
                        productAttribute.Id
                    );

                    await _unitOfWork.GetRepository<ProductProductAttribute>()
                        .InsertAsync(productProductAttribute);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

                return ResultModel<long>.Success(productAttribute.Id);
            }
            catch (DomainException ex)
            {
                return ResultModel<long>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return ResultModel<long>.Fail("خطا در ایجاد ویژگی: " + ex.Message);
            }
        }
    }
}
