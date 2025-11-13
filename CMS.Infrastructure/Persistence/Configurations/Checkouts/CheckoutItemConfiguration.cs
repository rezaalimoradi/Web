using CMS.Domain.Checkouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Checkouts
{
    public class CheckoutItemConfiguration : IEntityTypeConfiguration<CheckoutItem>
    {
        public void Configure(EntityTypeBuilder<CheckoutItem> builder)
        {
            builder.ToTable("CheckoutItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.UnitPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(i => i.Quantity)
                   .IsRequired();

            // روابط با Checkout
            builder.HasOne(i => i.Checkout)
                   .WithMany(c => c.Items)
                   .HasForeignKey(i => i.CheckoutId)
                   .OnDelete(DeleteBehavior.Cascade);

            // روابط با Product
            builder.HasOne(i => i.Product)
                   .WithMany()
                   .HasForeignKey(i => i.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            // روابط با Translations
            builder.HasMany(i => i.Translations)
                   .WithOne(t => t.CheckoutItem)
                   .HasForeignKey(t => t.CheckoutItemId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
