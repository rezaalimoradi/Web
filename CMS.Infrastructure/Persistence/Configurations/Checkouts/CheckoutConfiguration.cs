using CMS.Domain.Checkouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Checkouts
{
    public class CheckoutConfiguration : IEntityTypeConfiguration<Checkout>
    {
        public void Configure(EntityTypeBuilder<Checkout> builder)
        {
            builder.ToTable("Checkouts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CouponCode)
                   .HasMaxLength(450);

            builder.Property(c => c.CouponRuleName)
                   .HasMaxLength(450);

            builder.Property(c => c.ShippingMethod)
                   .HasMaxLength(450);

            builder.Property(c => c.OrderNote)
                   .HasMaxLength(1000);

            // Navigation: Customer
            builder.HasOne(c => c.Customer)
                   .WithMany()
                   .HasForeignKey(c => c.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Navigation: CreatedBy
            builder.HasOne(c => c.CreatedBy)
                   .WithMany()
                   .HasForeignKey(c => c.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);

            // Navigation: Items (1 -> many)
            builder.HasMany(c => c.Items)
                   .WithOne(i => i.Checkout)
                   .HasForeignKey(i => i.CheckoutId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Navigation: Translations (1 -> many)
            builder.HasMany(c => c.Translations)
                   .WithOne(t => t.Checkout)
                   .HasForeignKey(t => t.CheckoutId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
