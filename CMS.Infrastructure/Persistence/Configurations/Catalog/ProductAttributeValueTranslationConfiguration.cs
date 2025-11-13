using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductAttributeValueTranslationConfiguration : IEntityTypeConfiguration<ProductAttributeValueTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValueTranslation> builder)
        {
            builder.ToTable("ProductAttributeValueTranslations");

            // 🔑 کلید اصلی
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // 🏷 فیلدها
            builder.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(true);

            // 🧩 روابط
            builder.HasOne(x => x.ProductAttributeValue)
                   .WithMany(v => v.Translations)
                   .HasForeignKey(x => x.ProductAttributeValueId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(x => x.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🚫 جلوگیری از تکرار ترجمه برای یک زبان
            builder.HasIndex(x => new { x.ProductAttributeValueId, x.WebSiteLanguageId })
                   .IsUnique()
                   .HasDatabaseName("IX_ProductAttributeValue_Language_Unique");
        }
    }
}
