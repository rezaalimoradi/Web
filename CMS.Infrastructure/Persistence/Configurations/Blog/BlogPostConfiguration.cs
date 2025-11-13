using CMS.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Persistence.Configurations.Blog
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.ToTable("BlogPosts");

            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Translations)
                .WithOne(t => t.BlogPost)
                .HasForeignKey(t => t.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
