using CMS.Application.Catalog.Queries;
using FluentValidation;

namespace CMS.Application.Catalog.Validators
{
    public class GetAllTaxQueryValidator : AbstractValidator<GetAllTaxQuery>
    {
        public GetAllTaxQueryValidator()
        {
        }
    }
}
