using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductAttributeTranslationConfiguration : IEntityTypeConfiguration<ProductAttributeTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeTranslation> builder)
        {
            builder.ToTable("ProductAttributeTranslations");

            // 🔑 کلید اصلی
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // 🏷 فیلدها
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(true);

            // 🧩 روابط
            builder.HasOne(x => x.ProductAttribute)
                   .WithMany(a => a.Translations)
                   .HasForeignKey(x => x.ProductAttributeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(x => x.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🚫 جلوگیری از تکرار ترجمه برای یک زبان
            builder.HasIndex(x => new { x.ProductAttributeId, x.WebSiteLanguageId })
                   .IsUnique()
                   .HasDatabaseName("IX_ProductAttribute_Language_Unique");
        }
    }
}
