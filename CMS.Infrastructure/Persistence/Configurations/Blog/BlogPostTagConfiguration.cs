using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Persistence.Configurations.Blog
{
    public class BlogPostTagConfiguration : IEntityTypeConfiguration<BlogPostTag>
    {
        public void Configure(EntityTypeBuilder<BlogPostTag> builder)
        {
            builder.ToTable("BlogPostTags");

            builder.HasKey(pt => new { pt.BlogPostId, pt.BlogTagId });

            builder.HasOne(pt => pt.BlogPost)
                .WithMany(p => p.Tags)
                .HasForeignKey(pt => pt.BlogPostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pt => pt.BlogTag)
                .WithMany(t => t.BlogPostTags)
                .HasForeignKey(pt => pt.BlogTagId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
