using CMS.Application.Blog.Queries;
using FluentValidation;

namespace CMS.Application.Blog.Validators
{
    public class GetBlogCategoriesByParentIdQueryValidator : AbstractValidator<GetBlogCategoriesByParentIdQuery>
    {
        public GetBlogCategoriesByParentIdQueryValidator()
        {
        }
    }
}
