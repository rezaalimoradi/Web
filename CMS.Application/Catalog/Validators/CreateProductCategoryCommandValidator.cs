using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
    {
        private readonly IRepository<ProductCategory> _categoryRepository;
        private readonly ITenantContext _tenantContext;

        public CreateProductCategoryCommandValidator(
            IRepository<ProductCategory> categoryRepository,
            ITenantContext tenantContext)
        {
            _categoryRepository = categoryRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            //RuleFor(x => x.Slug)
            //    .NotEmpty()
            //    .MaximumLength(256)
            //    .MustAsync(BeUniqueSlug).WithMessage("Slug must be unique for the current website.");

            //RuleFor(x => x.WebSiteLanguageId)
            //    .Equal(_tenantContext.TenantId).WithMessage("Invalid WebSiteLanguage id.");
        }

        private async Task<bool> BeUniqueSlug(CreateProductCategoryCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _categoryRepository.Table.AnyAsync(c =>
                c.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }
    }
}
