using FluentValidation;
using CMS.Application.Tenants.Commands;

namespace CMS.Application.Tenants.Validators
{
    public class CreateLanguageCommandValidator : AbstractValidator<CreateLanguageCommand>
    {
        public CreateLanguageCommandValidator()
        {
        }
    }
}
