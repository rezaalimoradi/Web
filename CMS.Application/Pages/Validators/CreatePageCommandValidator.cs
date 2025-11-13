using CMS.Application.Pages.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Pages.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Pages.Validators
{
    public class CreatePageCommandValidator : AbstractValidator<CreatePageCommand>
    {
        private readonly IRepository<PageTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public CreatePageCommandValidator(IRepository<PageTranslation> translationRepository, ITenantContext tenantContext)
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

            RuleFor(x => x.Content)
                .NotEmpty();
        }

        private async Task<bool> BeUniqueSlug(CreatePageCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _translationRepository.Table
                .AnyAsync(t =>
                    t.Slug == slug &&
                    t.WebSiteLanguageId == command.WebSiteLanguageId &&
                    t.Page.WebSiteId == _tenantContext.TenantId,
                    cancellationToken);
        }
    }
}
