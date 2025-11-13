using CMS.Domain.Catalog.Entities;
using CMS.Domain.Media.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            // --- کلید اصلی ---
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            // --- خصوصیات پایه ---
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.SKU)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(p => p.Barcode)
                .HasMaxLength(128);

            // --- Default Values ---
            builder.Property(p => p.IsPublished).HasDefaultValue(false);
            builder.Property(p => p.ShowOnHomepage).HasDefaultValue(false);
            builder.Property(p => p.AllowCustomerReviews).HasDefaultValue(false);
            builder.Property(p => p.IsCallForPrice).HasDefaultValue(false);

            // --- روابط اصلی ---
            builder.HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Tax)
                .WithMany()
                .HasForeignKey(p => p.TaxId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.WebSite)
                .WithMany()
                .HasForeignKey(p => p.WebSiteId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- ترجمه‌ها ---
            builder.HasMany(p => p.Translations)
                .WithOne(t => t.Product)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- ویژگی‌های محصول ---
            builder.HasMany(p => p.ProductProductAttributes)
                .WithOne(pa => pa.Product)
                .HasForeignKey(pa => pa.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- دسته‌بندی‌های چندگانه ---
            builder.HasMany(p => p.Product_ProductCategories)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            // --- محصولات مرتبط ---
            builder.HasMany(p => p.RelatedProducts)
                .WithOne(rp => rp.Product)
                .HasForeignKey(rp => rp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- MediaAttachments (Generic) - مثل ProductCategory ---
            builder.HasMany(p => p.MediaAttachments)
                .WithOne() // MediaAttachment.EntityId generic, no direct navigation back
                .HasForeignKey(m => m.EntityId)
                .HasPrincipalKey(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // --- ایندکس برای بهبود عملکرد جستجوی MediaAttachments ---
            builder.HasIndex(p => p.Id); // برای Join با MediaAttachment.EntityId
        }
    }
}