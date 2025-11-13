using CMS.Application.CompareItems.Queries;
using CMS.Application.Wishlists.Queries;
using FluentValidation;

namespace CMS.Application.CompareItems.Validators
{
    public class GetCompareByCustomerQueryValidator : AbstractValidator<GetCompareByCustomerQuery>
    {
        public GetCompareByCustomerQueryValidator()
        {
        }
    }
}
