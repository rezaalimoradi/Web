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
    public class UpdateCartItemTranslationCommandValidator : AbstractValidator<UpdateCartItemTranslationCommand>
    {
        private readonly IRepository<CartItemTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public UpdateCartItemTranslationCommandValidator(IRepository<CartItemTranslation> translationRepository, ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.CartId)
                .GreaterThan(0)
                .MustAsync(ExistTranslationInTenant).WithMessage("Cart item translation not found or does not belong to tenant.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);
        }

        private async Task<bool> ExistTranslationInTenant(long id, CancellationToken ct)
        {
            return await _translationRepository.Table
                .AnyAsync(t => t.Id == id && t.CartItem.Cart.WebSiteId == _tenantContext.TenantId, ct);
        }
    }

}
