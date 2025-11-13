using CMS.Application.Menus.Queries;
using FluentValidation;

namespace CMS.Application.Blog.Validators
{
    public class GetMenuItemByIdQueryValidator : AbstractValidator<GetMenuItemByIdQuery>
    {
        public GetMenuItemByIdQueryValidator()
        {
        }
    }
}
