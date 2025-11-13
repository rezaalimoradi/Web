using CMS.Domain.Shippings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shippings
{
    public class ShippingRateConfiguration : IEntityTypeConfiguration<ShippingRate>
    {
        public void Configure(EntityTypeBuilder<ShippingRate> builder)
        {
            builder.ToTable("ShippingRates");

            builder.HasKey(r => r.Id);

            // Properties
            builder.Property(r => r.Rate)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(r => r.MinimumOrderAmount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(r => r.IsEnabled)
                   .IsRequired();

            builder.Property(r => r.CreatedOn)
                   .IsRequired();

            builder.Property(r => r.LatestUpdatedOn)
                   .IsRequired();

            // Relations
            builder.HasMany(r => r.Translations)
                   .WithOne(t => t.ShippingRate)
                   .HasForeignKey(t => t.ShippingRateId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
