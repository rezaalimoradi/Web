using CMS.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Persistence.Configurations.Blog
{
    public class BlogPostTranslationConfiguration : IEntityTypeConfiguration<BlogPostTranslation>
    {
        public void Configure(EntityTypeBuilder<BlogPostTranslation> builder)
        {
            builder.ToTable("BlogPostTranslations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(t => t.Slug)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(t => t.Summary)
                .HasMaxLength(1000);

            builder.Property(t => t.Content)
                .IsRequired();

            builder.Property(t => t.SeoTitle)
                .HasMaxLength(256);

            builder.Property(t => t.SeoDescription)
                .HasMaxLength(512);

            builder.HasOne(x => x.WebSiteLanguage)
                .WithMany()
                .HasForeignKey(x => x.WebSiteLanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => new { t.BlogPostId, t.WebSiteLanguageId })
                .IsUnique();

            builder.HasIndex(t => new { t.Slug, t.WebSiteLanguageId })
                .IsUnique();
        }
    }
}
