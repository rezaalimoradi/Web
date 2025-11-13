using CMS.Application.Catalog.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class GetBrandTranslationByIdQueryValidator : AbstractValidator<GetBrandTranslationByIdQuery>
    {
        private readonly IRepository<BrandTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public GetBrandTranslationByIdQueryValidator(IRepository<BrandTranslation> translationRepository, ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Brand translation not found.");
        }

        private async Task<bool> ExistInTenant(long id, CancellationToken cancellationToken)
        {
            return await _translationRepository.Table
                .Include(t => t.Brand)
                .AnyAsync(t => t.Id == id && t.Brand.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }
    }
}
