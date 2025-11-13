using CMS.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Blog
{
    public class BlogPostCategoryConfiguration : IEntityTypeConfiguration<BlogPostCategory>
    {
        public void Configure(EntityTypeBuilder<BlogPostCategory> builder)
        {
            builder.ToTable("BlogPostCategories");

            builder.HasKey(bpc => new { bpc.BlogPostId, bpc.BlogCategoryId });

            builder.HasOne(bpc => bpc.BlogPost)
                .WithMany(bp => bp.Categories)
                .HasForeignKey(bpc => bpc.BlogPostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bpc => bpc.BlogCategory)
                .WithMany(bc => bc.BlogPostCategories)
                .HasForeignKey(bpc => bpc.BlogCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
