using CMS.Application.Orders.Carts.Queries;
using FluentValidation;

namespace CMS.Application.Orders.Carts.Validators
{
    public class GetCartByCustomerQueryValidator : AbstractValidator<GetCartByCustomerQuery>
    {
        public GetCartByCustomerQueryValidator()
        {
        }
    }
}
