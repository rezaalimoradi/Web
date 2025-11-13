using CMS.Domain.Shippings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shippings
{
    public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.ToTable("Shippings");

            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.SystemName)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(s => s.IsEnabled)
                   .IsRequired();

            builder.Property(s => s.CreatedOn)
                   .IsRequired();

            builder.Property(s => s.LatestUpdatedOn)
                   .IsRequired();

            // Navigation: FreeRules
            builder.HasMany(s => s.FreeRules)
                   .WithOne()
                   .HasForeignKey("ShippingId")
                   .OnDelete(DeleteBehavior.Cascade);

            // Navigation: Rates
            builder.HasMany(s => s.Rates)
                   .WithOne()
                   .HasForeignKey("ShippingId")
                   .OnDelete(DeleteBehavior.Cascade);

            // Navigation: Translations
            builder.HasMany(s => s.Translations)
                   .WithOne(t => t.Shipping)
                   .HasForeignKey(t => t.ShippingId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
