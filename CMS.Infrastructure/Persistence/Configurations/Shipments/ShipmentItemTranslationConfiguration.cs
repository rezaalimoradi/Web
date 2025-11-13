using CMS.Domain.Shipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shipments
{
    public class ShipmentItemTranslationConfiguration : IEntityTypeConfiguration<ShipmentItemTranslation>
    {
        public void Configure(EntityTypeBuilder<ShipmentItemTranslation> builder)
        {
            builder.ToTable("ShipmentItemTranslations");

            builder.HasKey(t => t.Id);

            // روابط
            builder.HasOne(t => t.ShipmentItem)
                   .WithMany(s => s.Translations)
                   .HasForeignKey(t => t.ShipmentItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(t => t.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ستون‌ها
            builder.Property(t => t.Label)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(t => t.Note)
                   .HasMaxLength(1000);
        }
    }
}
