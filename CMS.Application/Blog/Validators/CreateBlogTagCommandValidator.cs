using CMS.Application.Blog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.Validators
{
    public class CreateBlogTagCommandValidator : AbstractValidator<CreateBlogTagCommand>
    {
        private readonly IRepository<BlogTagTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;
        public CreateBlogTagCommandValidator(IRepository<BlogTagTranslation> translationRepository, ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.WebSiteLanguageId).GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Slug)
                .NotEmpty()
                .MaximumLength(256)
                .MustAsync(BeUniqueSlug).WithMessage("Slug must be unique for the selected language and website.");
        }

        private async Task<bool> BeUniqueSlug(CreateBlogTagCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _translationRepository.Table
                .AnyAsync(t =>
                    t.Slug == slug &&
                    t.WebSiteLanguageId == command.WebSiteLanguageId &&
                    t.BlogTag.WebSiteId == _tenantContext.TenantId,
                    cancellationToken);
        }
    }
}
