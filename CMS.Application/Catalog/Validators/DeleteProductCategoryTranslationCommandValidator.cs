using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class DeleteProductCategoryTranslationCommandValidator : AbstractValidator<DeleteProductCategoryTranslationCommand>
    {
        private readonly IRepository<ProductCategoryTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public DeleteProductCategoryTranslationCommandValidator(
            IRepository<ProductCategoryTranslation> translationRepository,
            ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .MustAsync(TranslationExists).WithMessage("Translation not found or not accessible.");
        }

        private async Task<bool> TranslationExists(long id, CancellationToken cancellationToken)
        {
            return await _translationRepository.Table.AnyAsync(
                t => t.Id == id && t.ProductCategory.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }
    }
}
