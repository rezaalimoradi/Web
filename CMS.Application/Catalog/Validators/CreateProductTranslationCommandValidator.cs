using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class CreateProductTranslationCommandValidator : AbstractValidator<CreateProductTranslationCommand>
    {
        private readonly IRepository<ProductTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public CreateProductTranslationCommandValidator(
            IRepository<ProductTranslation> translationRepository,
            ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.LanguageId)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Slug)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(BeUniqueSlug).WithMessage("Slug must be unique for the selected language and product.");
        }

        private async Task<bool> BeUniqueSlug(CreateProductTranslationCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _translationRepository.Table.AnyAsync(t =>
                t.Slug == slug &&
                t.WebSiteLanguageId == command.LanguageId &&
                t.ProductId == command.ProductId &&
                t.Product.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }
    }
}
