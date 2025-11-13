using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductCategory> _categoryRepository;
        private readonly ITenantContext _tenantContext;

        public CreateProductCommandValidator(
            IRepository<Product> productRepository,
            IRepository<ProductCategory> categoryRepository,
            ITenantContext tenantContext)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.SKU)
                .NotEmpty()
                .MaximumLength(64)
                .MustAsync(BeUniqueSku).WithMessage("SKU must be unique for the current website.");

            //RuleFor(x => x.WebSiteId)
            //    .Equal(_tenantContext.TenantId).WithMessage("Invalid website id.");
        }

        private async Task<bool> BeValidCategory(long categoryId, CancellationToken cancellationToken)
        {
            return await _categoryRepository.Table.AnyAsync(c =>
                c.Id == categoryId && c.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }

        private async Task<bool> BeUniqueSku(CreateProductCommand command, string sku, CancellationToken cancellationToken)
        {
            return !await _productRepository.Table.AnyAsync(p =>
                p.SKU == sku && p.WebSiteId == _tenantContext.TenantId,
                cancellationToken);
        }
    }
}
