using CMS.Application.Orders.Carts.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Common;
using CMS.Domain.Orders.Carts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.Validators
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly ITenantContext _tenantContext;

        public CreateCartCommandValidator(IRepository<Cart> cartRepository, ITenantContext tenantContext)
        {
            _cartRepository = cartRepository;
            _tenantContext = tenantContext;

            RuleFor(x => x.CustomerIdentifier)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.WebSiteId)
                .GreaterThan(0)
                .Must(BeCorrectTenant).WithMessage("Invalid tenant for website.");
        }

        private bool BeCorrectTenant(long webSiteId)
        {
            // مثال: اگر لازم بود اینجا چک بیشتری برای وبسایت انجام بدی
            return webSiteId == _tenantContext.TenantId;
        }
    }
}
