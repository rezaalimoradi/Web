using CMS.Application.Menus.Commands;
using FluentValidation;

namespace CMS.Application.Menus.Validators
{
    public class CreateMenuItemCommandValidator : AbstractValidator<CreateMenuItemCommand>
    {
        public CreateMenuItemCommandValidator()
        {
            RuleFor(x => x.WebSiteLanguageId)
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.MenuId)
                .GreaterThan(0);
        }
    }
}
