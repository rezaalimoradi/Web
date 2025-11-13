using CMS.Application.Catalog.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class GetProductPricesQueryValidator : AbstractValidator<GetProductPricesQuery>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ITenantContext _tenantContext;

        public GetProductPricesQueryValidator(
            IRepository<Product> productRepository,
            ITenantContext tenantContext)
        {
            _productRepository = productRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .MustAsync(BeValidProduct).WithMessage("Product not found or does not belong to current website.");
        }

        private async Task<bool> BeValidProduct(long productId, CancellationToken cancellationToken)
        {
            return await _productRepository.Table.AnyAsync(p =>
                p.Id == productId && p.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }
    }
}
