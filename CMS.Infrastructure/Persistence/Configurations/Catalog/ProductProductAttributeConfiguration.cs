using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductProductAttributeConfiguration : IEntityTypeConfiguration<ProductProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductProductAttribute> builder)
        {
            builder.ToTable("ProductProductAttributes");

            // 🔑 کلید اصلی
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // ⚙️ خصوصیات اجباری
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.ProductAttributeId).IsRequired();

            // 🧩 روابط
            builder.HasOne(x => x.Product)
                   .WithMany(p => p.ProductProductAttributes)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProductAttribute)
                   .WithMany(a => a.ProductProductAttributes)
                   .HasForeignKey(x => x.ProductAttributeId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔗 رابطه با ValueMappings
            builder.HasMany(x => x.ValueMappings)
                   .WithOne(v => v.ProductProductAttribute)
                   .HasForeignKey(v => v.ProductProductAttributeId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🚫 جلوگیری از تکرار ProductId و ProductAttributeId
            builder.HasIndex(x => new { x.ProductId, x.ProductAttributeId })
                   .IsUnique()
                   .HasDatabaseName("IX_Product_ProductAttribute_Unique");
        }
    }
}
