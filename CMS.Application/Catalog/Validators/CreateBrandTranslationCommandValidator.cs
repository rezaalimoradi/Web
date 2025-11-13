using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class CreateBrandTranslationCommandValidator : AbstractValidator<CreateBrandTranslationCommand>
    {
        private readonly IRepository<BrandTranslation> _translationRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly ITenantContext _tenantContext;

        public CreateBrandTranslationCommandValidator(
            IRepository<BrandTranslation> translationRepository,
            IRepository<Brand> brandRepository,
            ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _brandRepository = brandRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.LanguageId).GreaterThan(0);
            RuleFor(x => x.BrandId).GreaterThan(0)
                .MustAsync(BeValidBrand).WithMessage("Brand not found or doesn't belong to current website.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Slug)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(BeUniqueSlug).WithMessage("Slug must be unique for the selected language and website.");
        }

        private async Task<bool> BeValidBrand(long brandId, CancellationToken cancellationToken)
        {
            return await _brandRepository.Table.AnyAsync(b =>
                b.Id == brandId && b.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }

        private async Task<bool> BeUniqueSlug(CreateBrandTranslationCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _translationRepository.Table.AnyAsync(t =>
                t.Slug == slug &&
                t.WebSiteLanguageId == command.LanguageId &&
                t.Brand.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }
    }
}
