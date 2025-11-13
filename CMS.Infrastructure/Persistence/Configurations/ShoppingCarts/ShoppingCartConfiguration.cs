using CMS.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.ShoppingCarts
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("ShoppingCarts");

            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.SubTotal)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(sc => sc.CreatedOn)
                   .IsRequired();

            builder.Property(sc => sc.LatestUpdatedOn)
                   .IsRequired();

            // روابط
            builder.HasOne(sc => sc.Customer)
                   .WithMany()
                   .HasForeignKey(sc => sc.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sc => sc.CreatedBy)
                   .WithMany()
                   .HasForeignKey(sc => sc.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(sc => sc.Items)
                   .WithOne(i => i.ShoppingCart)
                   .HasForeignKey(i => i.ShoppingCartId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(sc => sc.Translations)
                   .WithOne(t => t.ShoppingCart)
                   .HasForeignKey(t => t.ShoppingCartId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
