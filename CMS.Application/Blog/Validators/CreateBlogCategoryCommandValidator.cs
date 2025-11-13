using CMS.Application.Blog.Commands;
using FluentValidation;

namespace CMS.Application.Blog.Validators
{
    public class CreateBlogCategoryCommandValidator : AbstractValidator<CreateBlogCategoryCommand>
    {
        public CreateBlogCategoryCommandValidator()
        {
        }
    }
}
