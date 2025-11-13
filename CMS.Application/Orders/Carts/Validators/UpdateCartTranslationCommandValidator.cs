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
    public class UpdateCartTranslationCommandValidator : AbstractValidator<UpdateCartTranslationCommand>
    {
        private readonly IRepository<CartTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public UpdateCartTranslationCommandValidator(IRepository<CartTranslation> translationRepository, ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.CartId)
                .GreaterThan(0)
                .MustAsync(ExistTranslationInTenant).WithMessage("Cart translation not found or does not belong to tenant.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Description)
                .MaximumLength(1000);
        }

        private async Task<bool> ExistTranslationInTenant(long id, CancellationToken ct)
        {
            return await _translationRepository.Table
                .AnyAsync(t => t.Id == id && t.Cart.WebSiteId == _tenantContext.TenantId, ct);
        }
    }

}
