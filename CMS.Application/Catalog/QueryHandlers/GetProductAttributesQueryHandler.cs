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
    public class GetProductAttributesQueryHandler
        : IAppRequestHandler<GetProductAttributesQuery, ResultModel<List<ProductAttributeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductAttributesQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<ProductAttributeDto>>> Handle(GetProductAttributesQuery request, CancellationToken cancellationToken)
        {
            // 1) لود همه attributeهای سایت به همراه مقادیر، ترجمه‌ها و mappingها
            var attributes = await _unitOfWork.GetRepository<ProductAttribute>()
                .GetAllAsync(
                    predicate: a => a.WebSiteId == _tenantContext.TenantId,
                    func: q => q
                        .Include(a => a.Translations)
                        .Include(a => a.Values)
                            .ThenInclude(v => v.Translations)
                        .Include(a => a.ProductProductAttributes)
                            .ThenInclude(ppa => ppa.ValueMappings)
                                .ThenInclude(vm => vm.ProductAttributeValue)
                );

            // 2) فیلتر attributeهایی که برای محصول مورد نظر اختصاص داده شده‌اند
            var productAttributes = attributes
                .Where(a => a.ProductProductAttributes.Any(ppa => ppa.ProductId == request.ProductId))
                .ToList();

            // 3) نگاشت به DTO با بررسی دقیق اینکه هر Value برای آن product انتخاب شده یا خیر
            var result = productAttributes.Select(attr =>
            {
                var translationSet = request.LanguageId.HasValue
                    ? attr.Translations.Where(t => t.WebSiteLanguageId == request.LanguageId.Value).ToList()
                    : attr.Translations.ToList();

                // ppa مربوط به همین محصول (اگر وجود داشته باشد)
                var ppaForProduct = attr.ProductProductAttributes
                    .FirstOrDefault(ppa => ppa.ProductId == request.ProductId);

                var values = attr.Values.Select(v =>
                {
                    // بررسی اینکه value برای همین محصول انتخاب شده یا خیر
                    var isSelected = ppaForProduct != null &&
                                     (ppaForProduct.ValueMappings?.Any(vm => vm.ProductAttributeValueId == v.Id) ?? false);

                    return new ProductAttributeValueDto
                    {
                        Id = v.Id,
                        AttributeId = attr.Id,
                        IsSelected = isSelected,
                        Translations = v.Translations.Select(tv => new ProductAttributeValueTranslationDto
                        {
                            LanguageId = tv.WebSiteLanguageId,
                            Value = tv.Value
                        }).ToList()
                    };
                }).ToList();

                return new ProductAttributeDto
                {
                    Id = attr.Id,
                    AllowMultipleValues = attr.AllowMultipleValues,
                    Translations = translationSet.Select(t => new ProductAttributeTranslationDto
                    {
                        WebSiteLanguageId = t.WebSiteLanguageId,
                        Name = t.Name
                    }).ToList(),
                    Values = values
                };
            }).ToList();

            return ResultModel<List<ProductAttributeDto>>.Success(result);
        }
    }
}
