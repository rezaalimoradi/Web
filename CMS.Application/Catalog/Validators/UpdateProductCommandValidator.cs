using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IRepository<Product> _repository;
        private readonly ITenantContext _tenantContext;

        public UpdateProductCommandValidator(IRepository<Product> repository, ITenantContext tenantContext)
        {
            _repository = repository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Product not found.");

            RuleFor(x => x.SKU)
                .NotEmpty()
                .MaximumLength(50)
                .MustAsync(BeUniqueSku).WithMessage("SKU must be unique within the website.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);
        }

        private async Task<bool> ExistInTenant(long id, CancellationToken cancellationToken)
        {
            return await _repository.Table
                .AnyAsync(p => p.Id == id && p.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }

        private async Task<bool> BeUniqueSku(UpdateProductCommand command, string sku, CancellationToken cancellationToken)
        {
            return !await _repository.Table
                .AnyAsync(p => p.SKU == sku && p.WebSiteId == _tenantContext.TenantId && p.Id != command.Id, cancellationToken);
        }
    }
}
