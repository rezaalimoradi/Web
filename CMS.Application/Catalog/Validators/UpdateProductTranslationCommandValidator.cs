using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class UpdateProductTranslationCommandValidator : AbstractValidator<UpdateProductTranslationCommand>
    {
        private readonly IRepository<ProductTranslation> _translationRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly ITenantContext _tenantContext;

        public UpdateProductTranslationCommandValidator(
            IRepository<ProductTranslation> translationRepository,
            IRepository<Product> productRepository,
            ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _productRepository = productRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .MustAsync(ProductExists).WithMessage("Product not found or not accessible.");

            RuleFor(x => x.LanguageId)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Description)
                .MaximumLength(4000);

            RuleFor(x => x.Slug)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(BeUniqueSlug).WithMessage("Slug must be unique for the product and language.");
        }

        private async Task<bool> TranslationExists(long id, CancellationToken cancellationToken)
        {
            return await _translationRepository.Table
                .AnyAsync(t => t.Id == id && t.Product.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }

        private async Task<bool> ProductExists(long productId, CancellationToken cancellationToken)
        {
            return await _productRepository.Table
                .AnyAsync(p => p.Id == productId && p.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }

        private async Task<bool> BeUniqueSlug(UpdateProductTranslationCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _translationRepository.Table
                .AnyAsync(t =>
                    t.Slug == slug &&
                    t.ProductId == command.ProductId &&
                    t.WebSiteLanguageId == command.LanguageId ,
                    cancellationToken);
        }
    }
}
