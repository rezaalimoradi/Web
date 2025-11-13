using CMS.Application.Catalog.Queries;
using FluentValidation;

namespace CMS.Application.Catalog.Validators
{
    public class GetProductAttributesByProductIdQueryValidator : AbstractValidator<GetProductAttributesByProductIdQuery>
    {
        public GetProductAttributesByProductIdQueryValidator()
        {
        }
    }
}
