using CMS.Application.Catalog.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Validators
{
    public class GetTaxTranslationByIdQueryValidator : AbstractValidator<GetTaxTranslationByIdQuery>
    {
        private readonly IRepository<TaxTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public GetTaxTranslationByIdQueryValidator(
            IRepository<TaxTranslation> translationRepository,
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
                t => t.Id == id && t.Tax.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }
    }
}
