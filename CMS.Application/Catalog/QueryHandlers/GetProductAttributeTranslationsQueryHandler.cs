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
    public class GetProductAttributeTranslationsQueryHandler
        : IAppRequestHandler<GetProductAttributeTranslationsQuery, ResultModel<List<ProductAttributeTranslationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductAttributeTranslationsQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<ProductAttributeTranslationDto>>> Handle(GetProductAttributeTranslationsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Product>();

            var translations = await repo.GetAllAsync(
                predicate: a => a.Id == request.AttributeId
                                && a.Id == request.ProductId
                                && a.WebSiteId == _tenantContext.TenantId,
                func: q => q.Include(a => a.Translations)
            );

            var attribute = translations.FirstOrDefault();
            if (attribute == null)
                return ResultModel<List<ProductAttributeTranslationDto>>.Fail("Attribute not found.");

            var result = attribute.Translations
                .Select(t => new ProductAttributeTranslationDto
                {
                    WebSiteLanguageId = t.WebSiteLanguageId,
                    Name = t.Name
                })
                .ToList();

            return ResultModel<List<ProductAttributeTranslationDto>>.Success(result);
        }
    }
}
