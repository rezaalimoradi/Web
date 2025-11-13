using CMS.Application.Catalog.Queries;
using FluentValidation;

namespace CMS.Application.Catalog.Validators
{
    public class GetAllProductCategoryQueryValidator : AbstractValidator<GetAllProductCategoryQuery>
    {
        public GetAllProductCategoryQueryValidator()
        {
        }
    }
}
