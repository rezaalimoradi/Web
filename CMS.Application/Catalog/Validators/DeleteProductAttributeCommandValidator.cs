using CMS.Application.Catalog.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Catalog.Validators
{
    public class RemoveProductAttributeCommandValidator : AbstractValidator<DeleteProductAttributeCommand>
    {
        public RemoveProductAttributeCommandValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
        }
    }
}
