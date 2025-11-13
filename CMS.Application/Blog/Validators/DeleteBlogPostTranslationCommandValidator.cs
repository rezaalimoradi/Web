using CMS.Application.Blog.Commands;
using FluentValidation;

namespace CMS.Application.Blog.Validators
{
    public class DeleteBlogPostTranslationCommandValidator : AbstractValidator<DeleteBlogPostTranslationCommand>
    {
        public DeleteBlogPostTranslationCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.BlogPostId).GreaterThan(0);
        }
    }
}
