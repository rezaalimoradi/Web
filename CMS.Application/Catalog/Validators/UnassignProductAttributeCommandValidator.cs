using CMS.Application.Catalog.Commands;
using CMS.Application.Catalog.Queries;
using FluentValidation;

namespace CMS.Application.Catalog.Validators
{
    public class UnassignProductAttributeCommandValidator : AbstractValidator<UnassignProductAttributeCommand>
    {
        public UnassignProductAttributeCommandValidator()
        {
        }
    }
}
