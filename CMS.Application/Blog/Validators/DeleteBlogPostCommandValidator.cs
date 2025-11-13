using CMS.Application.Blog.Commands;
using FluentValidation;

namespace CMS.Application.Blog.Validators
{
    public class DeleteBlogPostCommandValidator : AbstractValidator<DeleteBlogPostCommand>
    {
        public DeleteBlogPostCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
