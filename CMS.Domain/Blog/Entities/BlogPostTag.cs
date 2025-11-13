using CMS.Domain.Blog.Entities;

namespace CMS.Infrastructure.Persistence.Configurations.Blog
{
    public class BlogPostTag
    {
        public long BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public long BlogTagId { get; set; }
        public BlogTag BlogTag { get; set; }
    }

}
