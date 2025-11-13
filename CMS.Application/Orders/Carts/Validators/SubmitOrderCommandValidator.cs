using CMS.Application.Orders.Carts.Commands;
using FluentValidation;

namespace CMS.Application.Orders.Carts.Validators
{
    public class SubmitOrderCommandValidator : AbstractValidator<SubmitOrderCommand>
    {
        public SubmitOrderCommandValidator()
        {
        }
    }
}
