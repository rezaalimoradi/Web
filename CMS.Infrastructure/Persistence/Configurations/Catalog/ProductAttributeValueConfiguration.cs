using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder.ToTable("ProductAttributeValues");

            // 🔑 کلید اصلی
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // 💰 تنظیمات مربوط به قیمت
            builder.Property(x => x.PriceAdjustment)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired(false);

            // 📦 موجودی
            builder.Property(x => x.StockQuantity)
                   .IsRequired(false);

            // ⚙️ تنظیمات وضعیت و ترتیب
            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true)
                   .IsRequired();

            builder.Property(x => x.DisplayOrder)
                   .HasDefaultValue(0)
                   .IsRequired();

            // 🔗 ارتباط با ProductAttribute
            builder.HasOne(x => x.ProductAttribute)
                   .WithMany(a => a.Values)
                   .HasForeignKey(x => x.ProductAttributeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            // 🌐 ترجمه‌ها
            builder.HasMany(x => x.Translations)
                   .WithOne(t => t.ProductAttributeValue)
                   .HasForeignKey(t => t.ProductAttributeValueId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ⚡ ایندکس‌ها برای بهبود عملکرد
            builder.HasIndex(x => x.ProductAttributeId)
                   .HasDatabaseName("IX_ProductAttributeValue_ProductAttributeId");

            builder.HasIndex(x => new { x.ProductAttributeId, x.IsActive })
                   .HasDatabaseName("IX_ProductAttributeValue_Active");

            builder.HasIndex(x => x.DisplayOrder)
                   .HasDatabaseName("IX_ProductAttributeValue_DisplayOrder");
        }
    }
}
