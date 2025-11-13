using CMS.Application.Blog.Queries;
using FluentValidation;

namespace CMS.Application.Blog.Validators
{
    public class GetBlogPostByIdQueryValidator : AbstractValidator<GetBlogPostByIdQuery>
    {
        public GetBlogPostByIdQueryValidator()
        {
        }
    }
}
