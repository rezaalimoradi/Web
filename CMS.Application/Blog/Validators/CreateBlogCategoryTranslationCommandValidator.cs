using CMS.Application.Blog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.Validators
{
    public class CreateBlogCategoryTranslationCommandValidator : AbstractValidator<CreateBlogCategoryTranslationCommand>
    {
        private readonly IRepository<BlogCategoryTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public CreateBlogCategoryTranslationCommandValidator(IRepository<BlogCategoryTranslation> translationRepository, ITenantContext tenantContext)
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

            RuleFor(x => x.Description)
                .NotEmpty();
        }

        private async Task<bool> BeUniqueSlug(CreateBlogCategoryTranslationCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _translationRepository.Table
                .AnyAsync(t =>
                    t.Slug == slug &&
                    t.WebSiteLanguageId == command.WebSiteLanguageId &&
                    t.BlogCategory.WebSiteId == _tenantContext.TenantId,
                    cancellationToken);
        }
    }
}
