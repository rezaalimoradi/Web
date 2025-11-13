using CMS.Application.Catalog.Commands;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{
    public class CreateProductRelationCommandValidator : AbstractValidator<CreateProductRelationCommand>
    {
        public CreateProductRelationCommandValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
        }
    }
}
