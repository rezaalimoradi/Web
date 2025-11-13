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
    public class CreateProductAttributeValueCommandHandler
        : IAppRequestHandler<CreateProductAttributeValueCommand, ResultModel<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public CreateProductAttributeValueCommandHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<long>> Handle(CreateProductAttributeValueCommand request, CancellationToken cancellationToken)
        {
            var attribute = await _unitOfWork.GetRepository<ProductAttribute>()
                .GetAsync(
                    predicate: a => a.Id == request.ProductAttributeId,
                    include: a => a
                        .Include(x => x.Values)
                            .ThenInclude(v => v.Translations)
                        .Include(x => x.Translations)
                );

            if (attribute == null)
                return ResultModel<long>.Fail("ProductAttribute not found.");

            if (attribute.WebSiteId != _tenantContext.TenantId)
                return ResultModel<long>.Fail("Attribute does not belong to current tenant.");

            var createdValue = attribute.AddValue(request.WebSiteLanguageId, request.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultModel<long>.Success(createdValue.Id);
        }
    }
}
