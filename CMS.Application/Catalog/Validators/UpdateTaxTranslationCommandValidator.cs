using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class UpdateTaxTranslationCommandValidator : AbstractValidator<UpdateTaxTranslationCommand>
    {
        private readonly IRepository<TaxTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public UpdateTaxTranslationCommandValidator(
            IRepository<TaxTranslation> translationRepository,
            ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .MustAsync(TranslationExists).WithMessage("Translation not found or not accessible.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);
        }

        private async Task<bool> TranslationExists(long id, CancellationToken cancellationToken)
        {
            return await _translationRepository.Table.AnyAsync(
                t => t.Id == id && t.Tax.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }
    }
}
