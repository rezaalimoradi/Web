using CMS.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Persistence.Configurations.Blog
{
    public class BlogTagTranslationConfiguration : IEntityTypeConfiguration<BlogTagTranslation>
    {
        public void Configure(EntityTypeBuilder<BlogTagTranslation> builder)
        {
            builder.ToTable("BlogTagTranslations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(t => t.Slug)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasIndex(t => new { t.BlogTagId, t.WebSiteLanguageId })
                .IsUnique();

            builder.HasIndex(t => new { t.Slug, t.WebSiteLanguageId })
                .IsUnique();
        }
    }

}
