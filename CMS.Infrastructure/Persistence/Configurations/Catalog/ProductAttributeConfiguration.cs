using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("ProductAttributes");

            // 🔑 کلید اصلی
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // 🏷 روابط با WebSite
            builder.HasOne(x => x.WebSite)
                   .WithMany()
                   .HasForeignKey(x => x.WebSiteId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // ⚙️ ویژگی‌های اصلی
            builder.Property(x => x.AllowMultipleValues)
                   .IsRequired()
                   .HasDefaultValue(false);

            // 🌐 رابطه با Translations
            builder.HasMany(x => x.Translations)
                   .WithOne(t => t.ProductAttribute)
                   .HasForeignKey(t => t.ProductAttributeId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 💎 رابطه با Values
            builder.HasMany(x => x.Values)
                   .WithOne(v => v.ProductAttribute)
                   .HasForeignKey(v => v.ProductAttributeId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🧩 ایندکس برای جلوگیری از تکرار Attribute با نام مشابه در یک سایت (در آینده کاربردی است)
            builder.HasIndex(x => x.WebSiteId)
                   .HasDatabaseName("IX_ProductAttribute_WebSiteId");

            // ⚡ ایندکس برای جستجوهای سریع در AllowMultipleValues (مثلاً فیلتر ویژگی‌هایی که چندمقداری هستند)
            builder.HasIndex(x => x.AllowMultipleValues)
                   .HasDatabaseName("IX_ProductAttribute_AllowMultiple");
        }
    }
}
