using CMS.Domain.Shipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shipments
{
    public class ShipmentTranslationConfiguration : IEntityTypeConfiguration<ShipmentTranslation>
    {
        public void Configure(EntityTypeBuilder<ShipmentTranslation> builder)
        {
            builder.ToTable("ShipmentTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TrackingNumber)
                   .HasMaxLength(450);

            builder.HasOne(x => x.Shipment)
                   .WithMany(s => s.Translations)
                   .HasForeignKey(x => x.ShipmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(x => x.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
