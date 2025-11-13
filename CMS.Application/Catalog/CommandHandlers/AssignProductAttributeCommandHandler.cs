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
    public class AssignProductAttributeCommandHandler
        : IAppRequestHandler<AssignProductAttributeCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public AssignProductAttributeCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<bool>> Handle(AssignProductAttributeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.GetRepository<Product>()
                    .GetAsync(
                        predicate: p => p.Id == request.ProductId,
                        include: q => q.Include(p => p.Product_ProductCategories)
                    );

                if (product == null)
                    return ResultModel<bool>.Fail("Product not found.");

                var attribute = await _unitOfWork.GetRepository<ProductAttribute>()
                    .GetAsync(a => a.Id == request.AttributeId);

                if (attribute == null)
                    return ResultModel<bool>.Fail("Attribute not found.");

                if (attribute.WebSiteId != _tenantContext.TenantId)
                    return ResultModel<bool>.Fail("Attribute does not belong to current tenant.");

                var existing = await _unitOfWork.GetRepository<ProductProductAttribute>()
                    .GetAsync(pp => pp.ProductId == request.ProductId && pp.ProductAttributeId == request.AttributeId);

                if (existing != null)
                    return ResultModel<bool>.Fail("این ویژگی قبلاً به این محصول اختصاص داده شده است.");

                var entity = new ProductProductAttribute(request.ProductId, request.AttributeId);
                await _unitOfWork.GetRepository<ProductProductAttribute>().InsertAsync(entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResultModel<bool>.Success(true);
            }
            catch (DomainException ex)
            {
                return ResultModel<bool>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return ResultModel<bool>.Fail("خطا در اختصاص ویژگی: " + ex.Message);
            }
        }
    }
}
