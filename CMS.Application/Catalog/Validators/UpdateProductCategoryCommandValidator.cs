using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class UpdateProductCategoryCommandValidator : AbstractValidator<UpdateProductCategoryCommand>
    {
        private readonly IRepository<ProductCategory> _repository;
        private readonly ITenantContext _tenantContext;

        public UpdateProductCategoryCommandValidator(IRepository<ProductCategory> repository, ITenantContext tenantContext)
        {
            _repository = repository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Product category not found.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            //RuleFor(x => x.Slug)
            //    .NotEmpty()
            //    .MaximumLength(256)
            //    .MustAsync(BeUniqueSlug).WithMessage("Slug must be unique for the website.");
        }

        private async Task<bool> ExistInTenant(long id, CancellationToken cancellationToken)
        {
            return await _repository.Table
                .AnyAsync(c => c.Id == id && c.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }

        private async Task<bool> BeUniqueSlug(UpdateProductCategoryCommand command, string slug, CancellationToken cancellationToken)
        {
            return !await _repository.Table
                .AnyAsync(c => c.WebSiteId == _tenantContext.TenantId && c.Id != command.Id, cancellationToken);
        }
    }
}
