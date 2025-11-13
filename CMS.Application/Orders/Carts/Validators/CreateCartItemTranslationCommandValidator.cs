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
    public class CreateCartItemTranslationCommandValidator : AbstractValidator<CreateCartItemTranslationCommand>
    {
        private readonly IRepository<CartItemTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public CreateCartItemTranslationCommandValidator(IRepository<CartItemTranslation> translationRepository, ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.LanguageId)
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.CartItemId)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Cart item not found or does not belong to tenant.");
        }

        private async Task<bool> ExistInTenant(long cartItemId, CancellationToken cancellationToken)
        {
            return await _translationRepository.Table
                .AnyAsync(t => t.CartItemId == cartItemId && t.CartItem.Cart.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }
    }
}
