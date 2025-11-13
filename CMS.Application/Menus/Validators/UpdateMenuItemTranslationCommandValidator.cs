using CMS.Application.Menus.Commands;
using FluentValidation;

namespace CMS.Application.Menus.Validators
{
    public class UpdateMenuItemTranslationCommandValidator : AbstractValidator<UpdateMenuItemTranslationCommand>
    {
        public UpdateMenuItemTranslationCommandValidator()
        {
            RuleFor(x => x.WebSiteLanguageId).GreaterThan(0);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Link)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.MenuItemId)
                .GreaterThan(0);

        }
    }
}
