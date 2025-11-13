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
    internal class GetTaxByIdQueryHandler : IAppRequestHandler<GetTaxByIdQuery, ResultModel<TaxDto?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetTaxByIdQueryHandler(IUnitOfWork unitOfWork, ITenantContext tenantContext)
        {
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<ResultModel<TaxDto?>> Handle(GetTaxByIdQuery request, CancellationToken cancellationToken)
        {
            var tax = await _unitOfWork.GetRepository<Tax>().GetAsync(
                predicate: x => x.Id == request.Id && x.WebSiteId == _tenantContext.TenantId
            );

            if (tax == null)
                return ResultModel<TaxDto?>.Fail("Tax not found.");

            var dto = new TaxDto
            {
                Id = tax.Id,
                Rate = tax.Rate,
                IsActive = tax.IsActive,
                WebSiteId = tax.WebSiteId
            };

            return ResultModel<TaxDto?>.Success(dto);
        }
    }
}
