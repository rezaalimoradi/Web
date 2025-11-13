using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class CreateProductCategoryTranslationCommandValidator : AbstractValidator<CreateProductCategoryTranslationCommand>
    {
        private readonly IRepository<ProductCategoryTranslation> _translationRepository;
        private readonly IRepository<ProductCategory> _categoryRepository;
        private readonly ITenantContext _tenantContext;

        public CreateProductCategoryTranslationCommandValidator(
            IRepository<ProductCategoryTranslation> translationRepository,
            IRepository<ProductCategory> categoryRepository,
            ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _categoryRepository = categoryRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.ProductCategoryId)
                .GreaterThan(0)
                .MustAsync(ProductCategoryExists).WithMessage("Product category not found or not accessible.");

            RuleFor(x => x.WebSiteLanguageId)
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.Slug)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(BeUniqueSlug).WithMessage("Slug must be unique for the selected language and website.");
        }

        private async Task<bool> ProductCategoryExists(long productCategoryId, CancellationToken cancellationToken)
        {
            return await _categoryRepository.Table.AnyAsync(
                c => c.Id == productCategoryId && c.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }

        private async Task<bool> BeUniqueSlug(CreateProductCategoryTranslationCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _translationRepository.Table.AnyAsync(
                t => t.Slug == slug &&
                     t.WebSiteLanguageId == command.WebSiteLanguageId &&
                     t.ProductCategory.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }
    }
}
