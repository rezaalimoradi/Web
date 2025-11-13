using CMS.Application.Catalog.Queries;
using FluentValidation;

namespace CMS.Application.Catalog.Validators
{
    public class GetBrandsByCategoryQueryValidator : AbstractValidator<GetBrandsByCategoryQuery>
    {
        public GetBrandsByCategoryQueryValidator()
        {
        }
    }
}
