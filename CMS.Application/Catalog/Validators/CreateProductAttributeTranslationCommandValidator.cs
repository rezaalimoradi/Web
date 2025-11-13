using CMS.Application.Catalog.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Validators
{
    public class CreateProductAttributeTranslationCommandValidator : AbstractValidator<CreateProductAttributeTranslationCommand>
    {
        public CreateProductAttributeTranslationCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId is required.");

            RuleFor(x => x.AttributeId)
                .GreaterThan(0).WithMessage("AttributeId is required.");

            RuleFor(x => x.LanguageId)
                .GreaterThan(0).WithMessage("LanguageId is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must be less than 200 characters.");
        }
    }
}
