using CMS.Application.Catalog.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Validators
{
    public class AddProductAttributeValueCommandValidator : AbstractValidator<CreateProductAttributeValueCommand>
    {
        public AddProductAttributeValueCommandValidator()
        {
            RuleFor(x => x.ProductAttributeId)
                .GreaterThan(0).WithMessage("AttributeId must be greater than 0.");

            RuleFor(x => x.WebSiteLanguageId)
                .GreaterThan(0).WithMessage("LanguageId must be greater than 0.");

            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Value is required.")
                .MaximumLength(200).WithMessage("Value must not exceed 200 characters.");
        }
    }
}
