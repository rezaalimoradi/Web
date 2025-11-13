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
    public class CreateCartItemCommandValidator : AbstractValidator<CreateCartItemCommand>
    {
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly ITenantContext _tenantContext;

        public CreateCartItemCommandValidator(IRepository<CartItem> cartItemRepository, ITenantContext tenantContext)
        {
            _cartItemRepository = cartItemRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.CartId)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Cart not found or does not belong to tenant.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Discount.HasValue);
        }

        private async Task<bool> ExistInTenant(long cartId, CancellationToken cancellationToken)
        {
            return await _cartItemRepository.Table
                .AnyAsync(ci => ci.CartId == cartId && ci.Cart.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }
    }
}
