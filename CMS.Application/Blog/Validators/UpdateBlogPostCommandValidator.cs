using CMS.Application.Blog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Blog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Blog.Validators
{
    public class UpdateBlogPostCommandValidator : AbstractValidator<UpdateBlogPostCommand>
    {
        private readonly IRepository<BlogPostTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;
        public UpdateBlogPostCommandValidator(IRepository<BlogPostTranslation> translationRepository, ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id).GreaterThan(0);
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

            RuleFor(x => x.Summary)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrEmpty(x.Summary));

            RuleFor(x => x.SeoTitle)
                .MaximumLength(256)
                .When(x => !string.IsNullOrEmpty(x.SeoTitle));

            RuleFor(x => x.SeoDescription)
                .MaximumLength(512)
                .When(x => !string.IsNullOrEmpty(x.SeoDescription));

            RuleFor(x => x.MetaKeywords)
                .MaximumLength(256)
                .When(x => !string.IsNullOrEmpty(x.MetaKeywords));

            RuleFor(x => x.CanonicalUrl)
                .MaximumLength(512)
                .When(x => !string.IsNullOrEmpty(x.CanonicalUrl));

            RuleForEach(x => x.CategoryIds).GreaterThan(0);
            RuleForEach(x => x.TagIds).GreaterThan(0);
        }

        private async Task<bool> BeUniqueSlug(UpdateBlogPostCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _translationRepository.Table
                .AnyAsync(t =>
                    t.Slug == slug &&
                    t.WebSiteLanguageId == command.WebSiteLanguageId &&
                    t.BlogPost.WebSiteId == _tenantContext.TenantId &&
                    t.BlogPostId != command.Id,
                    cancellationToken);
        }
    }
}
