using FluentValidation;
using CMS.Application.Users.Queries;

namespace CMS.Application.Users.Validators
{
    public class GetRolePermissionByRoleNameQueryValidator : AbstractValidator<GetRolePermissionByRoleNameQuery>
    {
        public GetRolePermissionByRoleNameQueryValidator()
        {
        }
    }
}
