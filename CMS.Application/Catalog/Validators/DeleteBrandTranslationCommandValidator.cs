using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class DeleteBrandTranslationCommandValidator : AbstractValidator<DeleteBrandTranslationCommand>
    {
        private readonly IRepository<BrandTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public DeleteBrandTranslationCommandValidator(IRepository<BrandTranslation> translationRepository, ITenantContext tenantContext)
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
