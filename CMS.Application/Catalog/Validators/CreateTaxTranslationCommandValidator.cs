using CMS.Application.Catalog.Commands;
using FluentValidation;

namespace CMS.Application.Catalog.Validators
{
    public class CreateTaxTranslationCommandValidator : AbstractValidator<CreateTaxTranslationCommand>
    {
        public CreateTaxTranslationCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.LanguageId)
                .GreaterThan(0);

            RuleFor(x => x.TaxId)
                .GreaterThan(0);
        }
    }
}
