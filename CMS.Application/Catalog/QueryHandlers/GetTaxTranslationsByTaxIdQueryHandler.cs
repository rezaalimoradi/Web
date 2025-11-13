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
    internal class GetTaxTranslationsByTaxIdQueryHandler : IAppRequestHandler<GetTaxTranslationsByTaxIdQuery, ResultModel<List<TaxTranslationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetTaxTranslationsByTaxIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<List<TaxTranslationDto>>> Handle(GetTaxTranslationsByTaxIdQuery request, CancellationToken cancellationToken)
        {
            // گرفتن ترجمه‌ها با چک سایت مرتبط
            var list = await _unitOfWork.GetRepository<TaxTranslation>().GetAllAsync(
                predicate: x => x.TaxId == request.TaxId
            );

            list = list.Where(x => x.Tax.WebSiteId == _tenantContext.TenantId).ToList();

            var dtos = list.Select(entity => new TaxTranslationDto
            {
                Id = entity.Id,
                TaxId = entity.TaxId,
                LanguageId = entity.WebSiteLanguageId,
                Name = entity.Name,
                Description = entity.Description
            }).ToList();

            return ResultModel<List<TaxTranslationDto>>.Success(dtos);
        }
    }
}
