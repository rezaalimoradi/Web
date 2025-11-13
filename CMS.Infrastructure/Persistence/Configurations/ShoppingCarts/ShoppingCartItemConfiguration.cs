using CMS.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.ShoppingCarts
{
    public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.ToTable("ShoppingCartItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.UnitPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(i => i.Quantity)
                   .IsRequired();

            builder.HasOne(i => i.ShoppingCart)
                   .WithMany(c => c.Items)
                   .HasForeignKey(i => i.ShoppingCartId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
