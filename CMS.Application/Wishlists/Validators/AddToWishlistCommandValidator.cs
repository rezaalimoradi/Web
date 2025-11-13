using CMS.Application.Wishlists.Commands;
using CMS.Application.Wishlists.Queries;
using FluentValidation;

namespace CMS.Application.Wishlists.Validators
{
    public class AddToWishlistCommandValidator : AbstractValidator<AddToWishlistCommand>
    {
        public AddToWishlistCommandValidator()
        {
        }
    }
}
