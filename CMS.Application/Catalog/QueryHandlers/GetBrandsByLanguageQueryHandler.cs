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
    internal class GetBrandsByLanguageQueryHandler
        : IAppRequestHandler<GetBrandsByLanguageQuery, ResultModel<List<BrandDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetBrandsByLanguageQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<BrandDto>>> Handle(GetBrandsByLanguageQuery request, CancellationToken cancellationToken)
        {
            var brandTranslations = await _unitOfWork.GetRepository<BrandTranslation>().GetAllAsync(
                predicate: t => t.WebSiteLanguageId == request.LanguageId
                                && t.Brand.WebSiteId == _tenantContext.TenantId,
                func: q => q.Include(t => t.Brand)
            );

            var dtos = brandTranslations.Select(t => new BrandDto
            {
                Id = t.BrandId,
                Title = t.Title,
                Slug = t.Slug,
                Description = t.Description
            }).ToList();

            return ResultModel<List<BrandDto>>.Success(dtos);
        }
    }
}
