using CMS.Application.Blog.Queries;
using FluentValidation;

namespace CMS.Application.Blog.Validators
{
    public class GetBlogCategoryByIdQueryValidator : AbstractValidator<GetBlogCategoryByIdQuery>
    {
        public GetBlogCategoryByIdQueryValidator()
        {
        }
    }
}
