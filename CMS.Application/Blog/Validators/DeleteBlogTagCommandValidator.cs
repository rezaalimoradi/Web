using CMS.Application.Blog.Commands;
using FluentValidation;

namespace CMS.Application.Blog.Validators
{
    public class DeleteBlogTagCommandValidator : AbstractValidator<DeleteBlogTagCommand>
    {
        public DeleteBlogTagCommandValidator()
        {
        }
    }
}
