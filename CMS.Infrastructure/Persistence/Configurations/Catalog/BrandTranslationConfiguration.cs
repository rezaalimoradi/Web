using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class BrandTranslationConfiguration : IEntityTypeConfiguration<BrandTranslation>
    {
        public void Configure(EntityTypeBuilder<BrandTranslation> builder)
        {
            builder.ToTable("BrandTranslations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(t => t.Slug)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(t => t.MetaTitle)
                .IsRequired(false)
                .HasMaxLength(2000);

            builder.Property(t => t.MetaDescription)
                .IsRequired(false)
                .HasMaxLength(2000);

            builder.Property(t => t.MetaKeywords)
                .IsRequired(false)
                .HasMaxLength(2000);

            builder.Property(t => t.CanonicalUrl)
                .IsRequired(false)
                .HasMaxLength(2000);

            builder.Property(t => t.Description)
                .IsRequired(false)
                .HasMaxLength(2000);

            builder.HasOne(x => x.WebSiteLanguage)
                .WithMany()
                .HasForeignKey(x => x.WebSiteLanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => new { t.BrandId, t.WebSiteLanguageId })
                .IsUnique();

            builder.HasIndex(t => new { t.Slug, t.WebSiteLanguageId })
                .IsUnique();
        }
    }
}
