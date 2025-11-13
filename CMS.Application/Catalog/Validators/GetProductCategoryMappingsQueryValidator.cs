using CMS.Application.Catalog.Queries;
using FluentValidation;

namespace CMS.Application.Catalog.Validators
{
    public class GetProductCategoryMappingsQueryValidator : AbstractValidator<GetProductCategoryMappingsQuery>
    {
        public GetProductCategoryMappingsQueryValidator()
        {
        }
    }
}
