using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductRelationConfiguration : IEntityTypeConfiguration<ProductRelation>
    {
        public void Configure(EntityTypeBuilder<ProductRelation> builder)
        {
            builder.ToTable("ProductRelations");

            builder.HasKey(pr => pr.Id);

            builder.HasOne(pr => pr.Product)
                   .WithMany(p => p.RelatedProducts)
                   .HasForeignKey(pr => pr.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pr => pr.RelatedProduct)
                   .WithMany()
                   .HasForeignKey(pr => pr.RelatedProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(pr => new { pr.ProductId, pr.RelatedProductId })
                   .IsUnique();
        }
    }
}
