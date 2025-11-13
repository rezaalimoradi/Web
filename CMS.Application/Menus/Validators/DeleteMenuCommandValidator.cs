using CMS.Application.Menus.Commands;
using FluentValidation;

namespace CMS.Application.Menus.Validators
{
    public class DeleteMenuCommandValidator : AbstractValidator<DeleteMenuCommand>
    {
        public DeleteMenuCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
