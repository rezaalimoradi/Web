using CMS.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Persistence.Configurations.Blog
{
    public class BlogCategoryTranslationConfiguration : IEntityTypeConfiguration<BlogCategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<BlogCategoryTranslation> builder)
        {
            builder.ToTable("BlogCategoryTranslations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(t => t.Slug)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(t => t.Description)
                .HasMaxLength(2000);

            builder.HasOne(x => x.WebSiteLanguage)
                .WithMany()
                .HasForeignKey(x => x.WebSiteLanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => new { t.BlogCategoryId, t.WebSiteLanguageId })
                .IsUnique();

            builder.HasIndex(t => new { t.Slug, t.WebSiteLanguageId })
                .IsUnique();
        }
    }
}
