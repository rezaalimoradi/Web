using CMS.Application.Catalog.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        private readonly IRepository<Product> _repository;
        private readonly ITenantContext _tenantContext;

        public GetProductByIdQueryValidator(IRepository<Product> repository, ITenantContext tenantContext)
        {
            _repository = repository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Product not found.");
        }

        private async Task<bool> ExistInTenant(long id, CancellationToken cancellationToken)
        {
            return await _repository.Table
                .AnyAsync(p => p.Id == id && p.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }
    }
}
