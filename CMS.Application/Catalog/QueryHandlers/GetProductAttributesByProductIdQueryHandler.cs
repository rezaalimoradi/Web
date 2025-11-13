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
    public class GetProductAttributesByProductIdQueryHandler
        : IAppRequestHandler<GetProductAttributesByProductIdQuery, ResultModel<List<ProductAttributeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductAttributesByProductIdQueryHandler(
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<ProductAttributeDto>>> Handle(GetProductAttributesByProductIdQuery request, CancellationToken cancellationToken)
        {
            if (request.ProductId <= 0)
                return ResultModel<List<ProductAttributeDto>>.Fail("شناسه محصول معتبر نیست.");

            var ppaRepo = _unitOfWork.GetRepository<ProductProductAttribute>();
            var ppas = await ppaRepo.GetAllAsync(
                predicate: x => x.ProductId == request.ProductId,
                func: q => q
                    .Include(x => x.ProductAttribute)
                        .ThenInclude(a => a.Translations)
                    .Include(x => x.ProductAttribute)
                        .ThenInclude(a => a.Values)
                            .ThenInclude(v => v.Translations)
                    .Include(x => x.ValueMappings)
            );

            if (ppas == null || !ppas.Any())
                return ResultModel<List<ProductAttributeDto>>.Success(new List<ProductAttributeDto>());

            var ppaIds = ppas.Select(p => p.Id).ToList();
            var mappingRepo = _unitOfWork.GetRepository<ProductProductAttribute_ValueMapping>();
            var mappings = await mappingRepo.GetAllAsync(
                predicate: m => ppaIds.Contains(m.ProductProductAttributeId),
                func: q => q.Include(m => m.ProductAttributeValue)
                            .ThenInclude(v => v.Translations)
            );

            var valueIdsLookup = mappings
                .Where(m => m.ProductAttributeValueId.HasValue)
                .GroupBy(m => m.ProductProductAttributeId)
                .ToDictionary(g => g.Key, g => g.Select(m => m.ProductAttributeValueId!.Value).ToHashSet());

            var customValuesLookup = mappings
                .Where(m => !string.IsNullOrWhiteSpace(m.CustomValue))
                .GroupBy(m => m.ProductProductAttributeId)
                .ToDictionary(g => g.Key, g => g.Select(m => m.CustomValue!).ToList());

            var result = new List<ProductAttributeDto>(ppas.Count());
            foreach (var ppa in ppas)
            {
                var attribute = ppa.ProductAttribute;
                if (attribute == null)
                    continue;

                valueIdsLookup.TryGetValue(ppa.Id, out var selectedValueIds);
                customValuesLookup.TryGetValue(ppa.Id, out var selectedCustomValues);

                var valuesDto = attribute.Values.Select(v => new ProductAttributeValueDto
                {
                    Id = v.Id,
                    AttributeId = v.ProductAttributeId,
                    Translations = v.Translations
                        .Where(t => !request.LanguageId.HasValue || t.WebSiteLanguageId == request.LanguageId.Value)
                        .Select(t => new ProductAttributeValueTranslationDto
                        {
                            LanguageId = t.WebSiteLanguageId,
                            Value = t.Value
                        }).ToList(),
                    IsSelected = selectedValueIds?.Contains(v.Id) == true
                }).ToList();

                var dto = new ProductAttributeDto
                {
                    Id = attribute.Id,
                    AllowMultipleValues = attribute.AllowMultipleValues,
                    Translations = attribute.Translations
                        .Where(t => !request.LanguageId.HasValue || t.WebSiteLanguageId == request.LanguageId.Value)
                        .Select(t => new ProductAttributeTranslationDto
                        {
                            WebSiteLanguageId = t.WebSiteLanguageId,
                            Name = t.Name
                        }).ToList(),
                    Values = valuesDto,
                    SelectedCustomValues = selectedCustomValues ?? new List<string>()
                };

                result.Add(dto);
            }

            return ResultModel<List<ProductAttributeDto>>.Success(result);
        }
    }
}
