using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Orders.Carts.Validators
{
    public class DeleteCartTranslationCommandValidator : AbstractValidator<DeleteCartTranslationCommand>
    {
        private readonly IRepository<CartTranslation> _translationRepository;
        private readonly ITenantContext _tenantContext;

        public DeleteCartTranslationCommandValidator(IRepository<CartTranslation> translationRepository, ITenantContext tenantContext)
        {
            _translationRepository = translationRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.CartId)
                .GreaterThan(0)
                .MustAsync(ExistTranslationInTenant).WithMessage("Cart translation not found or does not belong to tenant.");
        }

        private async Task<bool> ExistTranslationInTenant(long id, CancellationToken ct)
        {
            return await _translationRepository.Table
                .AnyAsync(t => t.Id == id && t.Cart.WebSiteId == _tenantContext.TenantId, ct);
        }
    }

}
