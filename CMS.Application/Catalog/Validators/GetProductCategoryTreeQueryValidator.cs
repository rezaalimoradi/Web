using CMS.Application.Catalog.Queries;
using FluentValidation;

namespace CMS.Application.Catalog.Validators
{
    public class GetProductCategoryTreeQueryValidator : AbstractValidator<GetProductCategoryTreeQuery>
    {
        public GetProductCategoryTreeQueryValidator()
        {
        }
    }
}
