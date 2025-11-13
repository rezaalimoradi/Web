using FluentValidation;
using CMS.Application.Tenants.Queries;

namespace CMS.Application.Tenants.Validators
{
    public class GetWebSiteByDomainQueryValidator : AbstractValidator<GetWebSiteByDomainQuery>
    {
        public GetWebSiteByDomainQueryValidator()
        {
        }
    }
}
