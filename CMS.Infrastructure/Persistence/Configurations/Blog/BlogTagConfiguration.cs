using CMS.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Blog
{
    public class BlogTagConfiguration : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.ToTable("BlogTags");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.HasMany(t => t.Translations)
                .WithOne(tr => tr.BlogTag)
                .HasForeignKey(tr => tr.BlogTagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
