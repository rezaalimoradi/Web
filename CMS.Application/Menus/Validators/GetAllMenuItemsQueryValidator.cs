using CMS.Application.Menus.Queries;
using FluentValidation;

namespace CMS.Application.Menus.Validators
{
    public class GetAllMenuItemsQueryValidator : AbstractValidator<GetAllMenuItemsQuery>
    {
        public GetAllMenuItemsQueryValidator()
        {
        }
    }
}
