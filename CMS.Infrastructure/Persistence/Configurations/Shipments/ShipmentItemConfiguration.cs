using CMS.Domain.Shipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shipments
{
    public class ShipmentItemConfiguration : IEntityTypeConfiguration<ShipmentItem>
    {
        public void Configure(EntityTypeBuilder<ShipmentItem> builder)
        {
            builder.ToTable("ShipmentItems");

            builder.HasKey(si => si.Id);

            // روابط
            builder.HasOne(si => si.Shipment)
                   .WithMany(s => s.Items)
                   .HasForeignKey(si => si.ShipmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(si => si.Product)
                   .WithMany()
                   .HasForeignKey(si => si.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            // اگر OrderItem به صورت navigation entity داشته باشی این رو فعال کن
            //builder.HasOne<OrderItem>() 
            //       .WithMany()
            //       .HasForeignKey(si => si.OrderItemId)
            //       .OnDelete(DeleteBehavior.Restrict);

            builder.Property(si => si.Quantity)
                   .IsRequired();

            // Translations
            builder.HasMany(si => si.Translations)
                   .WithOne(t => t.ShipmentItem)
                   .HasForeignKey(t => t.ShipmentItemId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
