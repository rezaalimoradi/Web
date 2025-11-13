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
    internal class GetBrandByIdQueryHandler : IAppRequestHandler<GetBrandByIdQuery, ResultModel<BrandDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetBrandByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<BrandDto>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.GetRepository<Brand>().GetAsync(
                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId,
                include: x => x.Include(y => y.Translations)
            );

            if (brand == null)
                return ResultModel<BrandDto>.Fail("Brand not found");

            var translation = brand.Translations.FirstOrDefault(t => t.Id == request.Id && t.Brand.WebSiteId == _tenantContext.TenantId);
            if (translation == null)
                return ResultModel<BrandDto>.Fail("Translation not found");

            return ResultModel<BrandDto>.Success(new BrandDto
            {
                Id = brand.Id,
                Title = translation.Title,
                Slug = translation.Slug,
                Description = translation.Description,
                CanonicalUrl = translation.CanonicalUrl,
                MetaDescription = translation.MetaDescription,
                MetaKeywords = translation.MetaKeywords,
                MetaTitle = translation.MetaTitle,
                WebSiteLanguageId = translation.WebSiteLanguageId
            });
        }
    }
}
