using CMS.Domain.Shipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shipments
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.ToTable("Shipments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TrackingNumber)
                   .HasMaxLength(450);

            builder.Property(x => x.CreatedOn)
                   .IsRequired();

            builder.Property(x => x.LatestUpdatedOn)
                   .IsRequired();

            builder.HasOne(x => x.Order)
                   .WithMany()
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CreatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Items)
                   .WithOne(i => i.Shipment)
                   .HasForeignKey(i => i.ShipmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Translations)
                   .WithOne(t => t.Shipment)
                   .HasForeignKey(t => t.ShipmentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
