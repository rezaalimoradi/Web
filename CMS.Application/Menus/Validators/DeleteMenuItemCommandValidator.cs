using CMS.Application.Menus.Commands;
using FluentValidation;

namespace CMS.Application.Menus.Validators
{
    public class DeleteMenuItemCommandValidator : AbstractValidator<DeleteMenuItemCommand>
    {
        public DeleteMenuItemCommandValidator()
        {
            RuleFor(x => x.MenuItemId)
                .GreaterThan(0);
        }
    }
}
