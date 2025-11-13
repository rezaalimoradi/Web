using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductCategoryTranslationConfiguration : IEntityTypeConfiguration<ProductCategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductCategoryTranslation> builder)
        {
            builder.ToTable("ProductCategoryTranslations");

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
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(t => new { t.ProductCategoryId, t.WebSiteLanguageId })
                .IsUnique();

            builder.HasIndex(t => new { t.Slug, t.WebSiteLanguageId })
                .IsUnique();
        }
    }
}
