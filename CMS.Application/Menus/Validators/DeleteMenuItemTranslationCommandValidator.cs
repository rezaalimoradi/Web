using CMS.Application.Menus.Commands;
using FluentValidation;

namespace CMS.Application.Menus.Validators
{
    public class DeleteMenuItemTranslationCommandValidator : AbstractValidator<DeleteMenuItemTranslationCommand>
    {
        public DeleteMenuItemTranslationCommandValidator()
        {
            RuleFor(x => x.MenuItemId)
                .GreaterThan(0);
        }
    }
}
