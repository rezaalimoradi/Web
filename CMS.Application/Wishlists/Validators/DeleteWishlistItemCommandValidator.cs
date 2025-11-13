using CMS.Application.Wishlists.Commands;
using CMS.Application.Wishlists.Queries;
using FluentValidation;

namespace CMS.Application.Wishlists.Validators
{
    public class DeleteWishlistItemCommandValidator : AbstractValidator<DeleteWishlistItemCommand>
    {
        public DeleteWishlistItemCommandValidator()
        {
        }
    }
}
