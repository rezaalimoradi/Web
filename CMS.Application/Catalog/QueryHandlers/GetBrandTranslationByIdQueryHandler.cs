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
    internal class GetBrandTranslationByIdQueryHandler
        : IAppRequestHandler<GetBrandTranslationByIdQuery, ResultModel<BrandTranslationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetBrandTranslationByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<BrandTranslationDto>> Handle(GetBrandTranslationByIdQuery request, CancellationToken cancellationToken)
        {
            var translation = await _unitOfWork.GetRepository<BrandTranslation>()
                .GetAsync(
                    predicate: x => x.Id == request.Id && x.Brand.WebSiteId == _tenantContext.TenantId,
                    include: q => q.Include(t => t.Brand)
                );

            if (translation is null)
                return ResultModel<BrandTranslationDto>.Fail("Translation not found");

            var dto = new BrandTranslationDto
            {
                Id = translation.Id,
                BrandId = translation.BrandId,
                LanguageId = translation.WebSiteLanguageId,
                Title = translation.Title,
                Slug = translation.Slug,
                Description = translation.Description
            };

            return ResultModel<BrandTranslationDto>.Success(dto);
        }
    }
}
