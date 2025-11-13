using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class DeleteProductCategoryCommandValidator : AbstractValidator<DeleteProductCategoryCommand>
    {
        private readonly IRepository<ProductCategory> _repository;
        private readonly ITenantContext _tenantContext;

        public DeleteProductCategoryCommandValidator(IRepository<ProductCategory> repository, ITenantContext tenantContext)
        {
            _repository = repository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Product category not found.");
        }

        private async Task<bool> ExistInTenant(long id, CancellationToken cancellationToken)
        {
            return await _repository.Table
                .AnyAsync(c => c.Id == id && c.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }
    }
}
