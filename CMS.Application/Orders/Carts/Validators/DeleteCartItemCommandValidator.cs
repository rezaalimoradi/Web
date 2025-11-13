using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.Validators
{
    public class DeleteCartItemCommandValidator : AbstractValidator<DeleteCartItemCommand>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly ITenantContext _tenantContext;

        public DeleteCartItemCommandValidator(IRepository<Cart> cartRepository, ITenantContext tenantContext)
        {
            _cartRepository = cartRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("شناسه محصول معتبر نیست.")
                .MustAsync(ExistCartItemForCustomer)
                .WithMessage("محصول مورد نظر در سبد خرید یافت نشد.");
        }

        private async Task<bool> ExistCartItemForCustomer(long productId, CancellationToken ct)
        {
            var tenantId = _tenantContext.TenantId;
            var customerIdentifier = _tenantContext.CustomerIdentifier ?? "guest";

            return await _cartRepository.Table
                .Include(c => c.Items)
                .AnyAsync(c =>
                    c.WebSiteId == tenantId &&
                    c.CustomerIdentifier == customerIdentifier &&
                    c.Items.Any(i => i.ProductId == productId),
                    ct);
        }
    }
}
