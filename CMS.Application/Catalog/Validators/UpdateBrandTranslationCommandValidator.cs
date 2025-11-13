using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class UpdateBrandTranslationCommandValidator : AbstractValidator<UpdateBrandTranslationCommand>
    {
        private readonly IRepository<BrandTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public UpdateBrandTranslationCommandValidator(IRepository<BrandTranslation> translationRepository, ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Brand translation not found.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Slug)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(BeUniqueSlug).WithMessage("Slug must be unique for the selected language and website.");
        }

        private async Task<bool> ExistInTenant(long id, CancellationToken cancellationToken)
        {
            return await _translationRepository.Table
                .Include(t => t.Brand)
                .AnyAsync(t => t.Id == id && t.Brand.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }

        private async Task<bool> BeUniqueSlug(UpdateBrandTranslationCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _translationRepository.Table
                .Include(t => t.Brand)
                .AnyAsync(t =>
                    t.Slug == slug &&
                    t.Brand.WebSiteId == _tenantContext.TenantId &&
                    t.Id != command.Id,
                    cancellationToken);
        }
    }
}
