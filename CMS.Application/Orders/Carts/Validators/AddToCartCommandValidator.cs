using CMS.Application.Orders.Carts.Commands;
using FluentValidation;

namespace CMS.Application.Orders.Carts.Validators
{
    public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        public AddToCartCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("شناسه محصول معتبر نیست.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("تعداد باید بیشتر از صفر باشد.");
        }
    }
}
