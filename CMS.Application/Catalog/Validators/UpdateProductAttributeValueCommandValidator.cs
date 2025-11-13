using CMS.Application.Catalog.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Validators
{
    public class UpdateProductAttributeValueCommandValidator : AbstractValidator<UpdateProductAttributeValueCommand>
    {
        public UpdateProductAttributeValueCommandValidator()
        {
            RuleFor(x => x.ProductAttributeId)
                .GreaterThan(0).WithMessage("AttributeValueId must be greater than 0.");

            RuleFor(x => x.WebSiteLanguageId)
                .GreaterThan(0).WithMessage("LanguageId must be greater than 0.");
        }
    }
}
