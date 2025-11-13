using CMS.Application.Blog.Queries;
using FluentValidation;

namespace CMS.Application.Blog.Validators
{
    public class GetBlogPostsPagedQueryValidator : AbstractValidator<GetBlogPostsPagedQuery>
    {
        public GetBlogPostsPagedQueryValidator()
        {
        }
    }
}
