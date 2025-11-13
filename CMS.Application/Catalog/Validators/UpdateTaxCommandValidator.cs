using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class UpdateTaxCommandValidator : AbstractValidator<UpdateTaxCommand>
    {
        private readonly IRepository<Tax> _taxRepository;
        private readonly ITenantContext _tenantContext;

        public UpdateTaxCommandValidator(IRepository<Tax> taxRepository, ITenantContext tenantContext)
        {
            _taxRepository = taxRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .MustAsync(TaxExists).WithMessage("Tax not found or not accessible.");

            RuleFor(x => x.Rate)
                .InclusiveBetween(0, 1);
        }

        private async Task<bool> TaxExists(long id, CancellationToken cancellationToken)
        {
            return await _taxRepository.Table.AnyAsync(
                x => x.Id == id && x.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }
    }
}
