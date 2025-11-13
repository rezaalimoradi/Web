using CMS.Application.Catalog.Queries;
using FluentValidation;

namespace CMS.Application.Catalog.Validators
{
    public class GetAllProductQueryValidator : AbstractValidator<GetAllProductQuery>
    {
        public GetAllProductQueryValidator()
        {
        }
    }
}
