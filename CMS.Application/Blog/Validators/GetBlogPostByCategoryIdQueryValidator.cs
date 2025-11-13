using CMS.Application.Blog.Queries;
using FluentValidation;

namespace CMS.Application.Blog.Validators
{
    public class GetBlogPostByCategoryIdQueryValidator : AbstractValidator<GetBlogPostByCategoryIdQuery>
    {
        public GetBlogPostByCategoryIdQueryValidator()
        {
        }
    }
}
