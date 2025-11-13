using CMS.Application.Blog.Commands;
using FluentValidation;

namespace CMS.Application.Blog.Validators
{
    public class UpdateBlogCategoryCommandValidator : AbstractValidator<UpdateBlogCategoryCommand>
    {
        public UpdateBlogCategoryCommandValidator()
        {
        }
    }
}
