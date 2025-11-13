using FluentValidation;
using CMS.Application.Tenants.Queries;

namespace CMS.Application.Tenants.Validators
{
    public class GetWebSiteLanguagesQueryValidator : AbstractValidator<GetWebSiteLanguagesQuery>
    {
        public GetWebSiteLanguagesQueryValidator()
        {
        }
    }
}
