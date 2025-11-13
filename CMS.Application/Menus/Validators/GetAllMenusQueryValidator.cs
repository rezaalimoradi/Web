using CMS.Application.Menus.Queries;
using FluentValidation;

namespace CMS.Application.Menus.Validators
{
    public class GetAllMenusQueryValidator : AbstractValidator<GetAllMenusQuery>
    {
        public GetAllMenusQueryValidator()
        {
        }
    }
}
