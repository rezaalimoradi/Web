using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.Validators
{
    public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly ITenantContext _tenantContext;

        public DeleteCartCommandValidator(IRepository<Cart> cartRepository, ITenantContext tenantContext)
        {
            _cartRepository = cartRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.CartId)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Cart not found or does not belong to tenant.");
        }

        private async Task<bool> ExistInTenant(long id, CancellationToken ct)
        {
            return await _cartRepository.Table
                .AnyAsync(c => c.Id == id && c.WebSiteId == _tenantContext.TenantId, ct);
        }
    }

}
