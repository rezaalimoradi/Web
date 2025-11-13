using FluentValidation;
using CMS.Application.Tenants.Queries;

namespace CMS.Application.Tenants.Validators
{
    public class GetDefaultLanguageCodeQueryValidator : AbstractValidator<GetDefaultLanguageCodeQuery>
    {
        public GetDefaultLanguageCodeQueryValidator()
        {
        }
    }
}
