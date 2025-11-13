namespace CMS.Domain.Blog.Entities
{
    public class BlogPostCategory
    {
        public long BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public long BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
