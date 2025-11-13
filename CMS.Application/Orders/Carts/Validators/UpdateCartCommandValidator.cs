using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.Validators
{
    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly ITenantContext _tenantContext;

        public UpdateCartCommandValidator(IRepository<Cart> cartRepository, ITenantContext tenantContext)
        {
            _cartRepository = cartRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.CartId)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Cart not found or does not belong to tenant.");

            RuleFor(x => x.CustomerIdentifier)
                .NotEmpty()
                .MaximumLength(256);
        }

        private async Task<bool> ExistInTenant(long id, CancellationToken ct)
        {
            return await _cartRepository.Table
                .AnyAsync(c => c.Id == id && c.WebSiteId == _tenantContext.TenantId, ct);
        }
    }

}
