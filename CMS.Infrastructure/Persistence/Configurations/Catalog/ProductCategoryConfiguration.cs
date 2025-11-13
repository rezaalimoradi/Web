using CMS.Domain.Catalog.Entities;
using CMS.Domain.Media.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");

            builder.HasKey(p => p.Id);

            // --- Translations ---
            builder.HasMany(p => p.Translations)
                .WithOne(t => t.ProductCategory)
                .HasForeignKey(t => t.ProductCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- Parent/Child relationship ---
            builder.HasOne(pc => pc.Parent)
                .WithMany(pc => pc.Children)
                .HasForeignKey(pc => pc.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- MediaAttachments (Generic) ---
            builder.HasMany(pc => pc.MediaAttachments)
                .WithOne() // MediaAttachment.EntityId generic, no direct navigation
                .HasForeignKey(m => m.EntityId)
                .HasPrincipalKey(pc => pc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pc => pc.Product_ProductCategories)
                .WithOne(m => m.ProductCategory)
                .HasForeignKey(m => m.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            // --- Indexes for MediaAttachments lookup ---
            builder.HasIndex(pc => new { pc.Id }); // optional, for performance
        }
    }
}
