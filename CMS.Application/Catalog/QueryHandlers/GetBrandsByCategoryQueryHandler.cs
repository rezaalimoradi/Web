using CMS.Application.Catalog.Dtos;
using CMS.Application.Catalog.Queries;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CMS.Application.Catalog.QueryHandlers
{
    internal class GetBrandsByCategoryQueryHandler
        : IAppRequestHandler<GetBrandsByCategoryQuery, ResultModel<List<BrandDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetBrandsByCategoryQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<BrandDto>>> Handle(GetBrandsByCategoryQuery request, CancellationToken cancellationToken)
        {
            // دریافت محصولات همراه با برندها مربوط به Tenant فعلی
            var products = await _unitOfWork
                .GetRepository<Product>()
                .GetAllAsync(
                    predicate: p => p.Product_ProductCategories.FirstOrDefault().ProductCategoryId == request.CategoryId
                                    && p.Brand != null
                                    && p.Brand.WebSiteId == _tenantContext.TenantId,
                    func: q => q.Include(p => p.Brand)
                                 .ThenInclude(b => b.Translations)
                );

            // استخراج برندهای یکتا
            var distinctBrands = products
                .Where(p => p.Brand != null)
                .Select(p => p.Brand)
                .DistinctBy(b => b.Id)
                .ToList();

            // تبدیل به DTO (با ترجمه اول موجود)
            var brandDtos = distinctBrands.Select(b =>
            {
                var translation = b.Translations.FirstOrDefault();
                return new BrandDto
                {
                    Id = b.Id,
                    Title = translation?.Title ?? string.Empty,
                    Slug = translation?.Slug ?? string.Empty,
                    Description = translation?.Description ?? string.Empty,
                    CanonicalUrl = translation?.CanonicalUrl ?? string.Empty,
                    MetaDescription = translation?.MetaDescription ?? string.Empty,
                    MetaKeywords = translation?.MetaKeywords ?? string.Empty,
                    MetaTitle = translation?.MetaTitle ?? string.Empty,
                    WebSiteLanguageId = translation?.WebSiteLanguageId ?? 0
                };
            }).ToList();

            return ResultModel<List<BrandDto>>.Success(brandDtos);
        }
    }
}
