using CMS.Application.Menus.Commands;
using FluentValidation;

namespace CMS.Application.Menus.Validators
{
    public class UpdateMenuCommandValidator : AbstractValidator<UpdateMenuCommand>
    {
        public UpdateMenuCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
