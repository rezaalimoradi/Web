using CMS.Domain.Shippings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shippings
{
    public class ShippingFreeConfiguration : IEntityTypeConfiguration<ShippingFree>
    {
        public void Configure(EntityTypeBuilder<ShippingFree> builder)
        {
            builder.ToTable("ShippingFreeRules");

            builder.HasKey(sf => sf.Id);

            // Properties
            builder.Property(sf => sf.MinimumOrderAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(sf => sf.IsEnabled)
                   .IsRequired();

            builder.Property(sf => sf.CreatedOn)
                   .IsRequired();

            builder.Property(sf => sf.LatestUpdatedOn)
                   .IsRequired();
        }
    }
}
