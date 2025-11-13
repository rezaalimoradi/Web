using CMS.Application.Catalog.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class GetProductTranslationByProductIdQueryValidator : AbstractValidator<GetAllProductTranslationByProductIdQuery>
    {
        private readonly IRepository<ProductTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public GetProductTranslationByProductIdQueryValidator(
            IRepository<ProductTranslation> translationRepository,
            ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .MustAsync(TranslationExists).WithMessage("Translation not found or not accessible.");
        }

        private async Task<bool> TranslationExists(long id, CancellationToken cancellationToken)
        {
            return await _translationRepository.Table.AnyAsync(
                t => t.ProductId == id && t.Product.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }
    }
}
