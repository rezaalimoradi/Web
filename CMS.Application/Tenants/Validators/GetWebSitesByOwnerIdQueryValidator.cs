using FluentValidation;
using CMS.Application.Tenants.Queries;

namespace CMS.Application.Tenants.Validators
{
    public class GetWebSitesByOwnerIdQueryValidator : AbstractValidator<GetWebSitesByOwnerIdQuery>
    {
        public GetWebSitesByOwnerIdQueryValidator()
        {
        }
    }
}
