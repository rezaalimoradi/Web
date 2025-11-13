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
    internal class GetProductCategoryByIdQueryHandler : IAppRequestHandler<GetProductCategoryByIdQuery, ResultModel<ProductCategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetProductCategoryByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<ProductCategoryDto>> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var productCategory = await _unitOfWork.GetRepository<ProductCategory>().GetAsync(
                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(y => y.Translations)
            );

            if (productCategory == null)
                return ResultModel<ProductCategoryDto>.Fail("ProductCategory not found");

            var translation = productCategory.Translations.FirstOrDefault(t => t.WebSiteLanguageId == _tenantContext.TenantId);
            if (translation == null)
                return ResultModel<ProductCategoryDto>.Fail("Translation not found");

            return ResultModel<ProductCategoryDto>.Success(new ProductCategoryDto
            {
                Id = productCategory.Id,
                Title = translation.Title,
                Slug = translation.Slug,
                Description = translation.Description,
                WebSiteLanguageId = _tenantContext.TenantId,    
                CanonicalUrl = translation.CanonicalUrl,
                MetaDescription = translation.MetaDescription,
                MetaKeywords = translation.MetaKeywords,
                MetaTitle = translation.MetaTitle
            });
        }
    }
}
