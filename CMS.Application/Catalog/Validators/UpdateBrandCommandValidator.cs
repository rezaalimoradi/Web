using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        private readonly IRepository<Brand> _brandRepository;
        private readonly ITenantContext _tenantContext;

        public UpdateBrandCommandValidator(IRepository<Brand> brandRepository, ITenantContext tenantContext)
        {
            _brandRepository = brandRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Brand not found.");

            RuleFor(x => x.LanguageId)
                .GreaterThan(0);

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
            return await _brandRepository.Table
                .AnyAsync(b => b.Id == id && b.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }

        private async Task<bool> BeUniqueSlug(UpdateBrandCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _brandRepository.Table
                .Include(b => b.Translations)
                .AnyAsync(b =>
                    b.Translations.Any(t => t.Slug == slug && t.WebSiteLanguageId == command.LanguageId) &&
                    b.WebSiteId == _tenantContext.TenantId &&
                    b.Id != command.Id,
                    cancellationToken);
        }
    }
}
