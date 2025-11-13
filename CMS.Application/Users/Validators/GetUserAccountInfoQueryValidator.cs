using FluentValidation;
using CMS.Application.Users.Queries;

namespace CMS.Application.Users.Validators
{
    public class GetUserAccountInfoQueryValidator : AbstractValidator<GetUserAccountInfoQuery>
    {
        public GetUserAccountInfoQueryValidator()
        {
        }
    }
}
