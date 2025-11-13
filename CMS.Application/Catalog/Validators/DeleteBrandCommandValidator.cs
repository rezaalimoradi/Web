using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        private readonly IRepository<Brand> _brandRepository;
        private readonly ITenantContext _tenantContext;

        public DeleteBrandCommandValidator(IRepository<Brand> brandRepository, ITenantContext tenantContext)
        {
            _brandRepository = brandRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Brand not found.");
        }

        private async Task<bool> ExistInTenant(long id, CancellationToken cancellationToken)
        {
            return await _brandRepository.Table
                .AnyAsync(b => b.Id == id && b.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }
    }
}
