using CMS.Application.Catalog.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Validators
{
    public class GetProductAttributeTranslationsQueryValidator : AbstractValidator<GetProductAttributeTranslationsQuery>
    {
        public GetProductAttributeTranslationsQueryValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId is required.");

            RuleFor(x => x.AttributeId)
                .GreaterThan(0).WithMessage("AttributeId is required.");
        }
    }
}
