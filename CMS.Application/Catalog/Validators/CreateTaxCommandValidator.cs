using CMS.Application.Catalog.Commands;
using FluentValidation;

namespace CMS.Application.Catalog.Validators
{
    public class CreateTaxCommandValidator : AbstractValidator<CreateTaxCommand>
    {
        public CreateTaxCommandValidator()
        {
            RuleFor(x => x.Rate)
                .InclusiveBetween(0, 1);

            RuleFor(x => x.WebSiteLanguageId)
                .GreaterThan(0);
        }
    }
}
