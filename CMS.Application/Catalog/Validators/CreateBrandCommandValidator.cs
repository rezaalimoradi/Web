using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        private readonly IRepository<BrandTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public CreateBrandCommandValidator(IRepository<BrandTranslation> translationRepository, ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.WebSiteLanguageId).GreaterThan(0);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Slug)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(BeUniqueSlug).WithMessage("Slug must be unique for the selected language and website.");
        }

        private async Task<bool> BeUniqueSlug(CreateBrandCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _translationRepository.Table
                .AnyAsync(t =>
                    t.Slug == slug &&
                    t.WebSiteLanguageId == command.WebSiteLanguageId &&
                    t.Brand.WebSiteId == _tenantContext.TenantId,
                    cancellationToken);
        }
    }
}
