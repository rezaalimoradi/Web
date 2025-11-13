using CMS.Application.Catalog.Queries;
using CMS.Application.Tenants;
using CMS.Domain.Catalog.Entities;
using CMS.Domain.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Catalog.Validators
{

    public class GetProductAttributeByIdQueryValidator : AbstractValidator<GetProductAttributeByIdQuery>
    {
        public GetProductAttributeByIdQueryValidator()
        {
        }
    }
}
