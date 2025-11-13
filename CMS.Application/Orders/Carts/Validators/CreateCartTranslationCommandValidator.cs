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
    public class CreateCartTranslationCommandValidator : AbstractValidator<CreateCartTranslationCommand>
    {
        private readonly IRepository<CartTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public CreateCartTranslationCommandValidator(IRepository<CartTranslation> translationRepository, ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.LanguageId)
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.CartId)
                .GreaterThan(0)
                .MustAsync(ExistInTenant).WithMessage("Cart not found or does not belong to tenant.");
        }

        private async Task<bool> ExistInTenant(long cartId, CancellationToken cancellationToken)
        {
            return await _translationRepository.Table
                .AnyAsync(t => t.CartId == cartId && t.Cart.WebSiteId == _tenantContext.TenantId, cancellationToken);
        }
    }
}
