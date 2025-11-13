using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductProductAttribute_ValueMappingConfiguration
        : IEntityTypeConfiguration<ProductProductAttribute_ValueMapping>
    {
        public void Configure(EntityTypeBuilder<ProductProductAttribute_ValueMapping> builder)
        {
            builder.ToTable("ProductProductAttribute_ValueMappings");

            // 🔑 کلید اصلی
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // 🧩 ارتباط با ProductProductAttribute (الزامی)
            builder.HasOne(x => x.ProductProductAttribute)
                   .WithMany(p => p.ValueMappings)
                   .HasForeignKey(x => x.ProductProductAttributeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            // 💎 ارتباط با ProductAttributeValue (اختیاری)
            builder.HasOne(x => x.ProductAttributeValue)
                   .WithMany(v => v.ProductProductAttribute_Values)
                   .HasForeignKey(x => x.ProductAttributeValueId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);

            // 📝 مقدار سفارشی (درصورت نبود مقدار از جدول AttributeValue)
            builder.Property(x => x.CustomValue)
                   .HasMaxLength(500)
                   .IsUnicode(true)
                   .IsRequired(false);

            // ⚡ ایندکس‌ها
            builder.HasIndex(x => new { x.ProductProductAttributeId, x.ProductAttributeValueId })
                   .HasDatabaseName("IX_ProductProductAttribute_Value")
                   .IsUnique(false);

            builder.HasIndex(x => x.ProductAttributeValueId)
                   .HasDatabaseName("IX_ProductAttributeValueId");

            // ⚙️ ایندکس کاربردی برای جستجوهای ترکیبی
            builder.HasIndex(x => new { x.ProductProductAttributeId, x.CustomValue })
                   .HasDatabaseName("IX_ProductProductAttribute_CustomValue");

            // ✅ اعتبارسنجی منطقی (در سطح Domain enforced)
            // یا ProductAttributeValueId باید مقدار داشته باشد یا CustomValue
            // در EF enforce نمی‌شود ولی در منطق دامنه بررسی می‌شود.
        }
    }
}
