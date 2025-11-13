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
    public class DeleteProductAttributeCommandHandler : IAppRequestHandler<DeleteProductAttributeCommand, ResultModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public DeleteProductAttributeCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<bool>> Handle(DeleteProductAttributeCommand request, CancellationToken cancellationToken)
        {
            var productAttribute = await _unitOfWork.GetRepository<ProductAttribute>()
                .GetAsync(
                    predicate: p => p.Id == request.id && p.WebSiteId == _tenantContext.TenantId,
                    include: p => p.Include(x => x.ProductProductAttributes)
                );

            if (productAttribute == null)
                return ResultModel<bool>.Fail("ProductAttribute not found.");

            try
            {
                productAttribute.RemoveValue(request.id);

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
