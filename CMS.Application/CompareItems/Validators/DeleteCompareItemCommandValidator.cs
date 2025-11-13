using CMS.Application.CompareItems.Commands;
using CMS.Application.CompareItems.Queries;
using CMS.Application.Wishlists.Queries;
using FluentValidation;

namespace CMS.Application.CompareItems.Validators
{
    public class DeleteCompareItemCommandValidator : AbstractValidator<DeleteCompareItemCommand>
    {
        public DeleteCompareItemCommandValidator()
        {
        }
    }
}
