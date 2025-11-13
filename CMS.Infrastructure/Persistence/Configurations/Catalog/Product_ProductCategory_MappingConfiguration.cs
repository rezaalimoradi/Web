using CMS.Domain.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Catalog
{
    public class Product_ProductCategory_MappingConfiguration : IEntityTypeConfiguration<Product_ProductCategory_Mapping>
    {
        public void Configure(EntityTypeBuilder<Product_ProductCategory_Mapping> builder)
        {
            builder.ToTable("Product_ProductCategory_Mappings");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Product_ProductCategories)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.ProductCategory)
                .WithMany(x => x.Product_ProductCategories)
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
