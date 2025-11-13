using CMS.Application.Catalog.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Validators
{
    public class UpdateProductAttributeCommandValidator : AbstractValidator<UpdateProductAttributeCommand>
    {
        public UpdateProductAttributeCommandValidator()
        {
            RuleFor(x => x.AttributeId).GreaterThan(0);
            RuleFor(x => x.WebSiteLanguageId).GreaterThan(0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);
        }
    }
}
