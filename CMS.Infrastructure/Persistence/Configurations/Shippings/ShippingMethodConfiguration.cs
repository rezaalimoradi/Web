using CMS.Domain.Shippings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shippings
{
    public class ShippingMethodConfiguration : IEntityTypeConfiguration<ShippingMethod>
    {
        public void Configure(EntityTypeBuilder<ShippingMethod> builder)
        {
            builder.ToTable("ShippingMethods");

            builder.HasKey(sm => sm.Id);

            // Relation → ShippingProvider
            builder.HasOne(sm => sm.ShippingProvider)
                   .WithMany(sp => sp.Methods) // مطمئن شو ShippingProvider.Methods تعریف شده
                   .HasForeignKey(sm => sm.ShippingProviderId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(sm => sm.BaseFee)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(sm => sm.EstimatedDeliveryDays)
                   .IsRequired();

            builder.Property(sm => sm.IsEnabled)
                   .IsRequired();

            // Relation → Translations
            builder.HasMany(sm => sm.Translations)
                   .WithOne(t => t.ShippingMethod)
                   .HasForeignKey(t => t.ShippingMethodId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
