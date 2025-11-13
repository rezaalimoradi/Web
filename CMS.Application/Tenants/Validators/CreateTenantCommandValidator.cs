using FluentValidation;
using CMS.Application.Tenants.Commands;

namespace CMS.Application.Tenants.Validators
{
    public class CreateWebSiteCommandValidator : AbstractValidator<CreateWebSiteCommand>
    {
        public CreateWebSiteCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
        }
    }
}
