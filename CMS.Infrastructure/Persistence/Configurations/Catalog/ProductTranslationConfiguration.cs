using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslations");


            builder.HasKey(p => p.Id);

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(t => t.SecondName)
                .IsRequired(false)
                .HasMaxLength(512);

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

            builder.Property(t => t.ShortDescription)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(t => t.Description)
                .IsRequired(false)
                .HasMaxLength(2000);

            builder.HasOne(x => x.WebSiteLanguage)
                .WithMany()
                .HasForeignKey(x => x.WebSiteLanguageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(t => new { t.ProductId, t.WebSiteLanguageId })
                .IsUnique();

            builder.HasIndex(t => new { t.Slug, t.WebSiteLanguageId })
                .IsUnique();
        }
    }
}
