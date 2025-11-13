using CMS.Application.Pages.Commands;
using FluentValidation;

namespace CMS.Application.Pages.Validators
{
    public class DeletePageCommandValidator : AbstractValidator<DeletePageCommand>
    {
        public DeletePageCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Page ID is required.");
        }
    }
}
