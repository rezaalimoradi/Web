using CMS.Application.Catalog.Dtos;
using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.QueryHandlers
{
    public class GetProductAttributeByIdQueryHandler
        : IAppRequestHandler<GetProductAttributeByIdQuery, ResultModel<ProductAttributeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductAttributeByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<ProductAttributeDto>> Handle(GetProductAttributeByIdQuery request, CancellationToken cancellationToken)
        {

            var repo = _unitOfWork.GetRepository<ProductAttribute>();

            var value = await repo.Table
                .Include(v => v.Translations)
                .Where(v => v.Id == request.AttributeId && v.WebSiteId == _tenantContext.TenantId)
                .FirstOrDefaultAsync(cancellationToken);

            if (value is null)
                return ResultModel<ProductAttributeDto>.Fail("Product Attribute not found.");

            var dto = MapToDto(value);

            return ResultModel<ProductAttributeDto>.Success(dto);
        }

        private static ProductAttributeDto MapToDto(ProductAttribute attribute)
        {
            return new ProductAttributeDto
            {
                Id = attribute.Id,
                Translations = attribute.Translations
                    .Select(t => new ProductAttributeTranslationDto
                    {
                        WebSiteLanguageId = t.WebSiteLanguageId,
                        Name = t.Name
                    })
                    .ToList()
            };
        }
    }
}
