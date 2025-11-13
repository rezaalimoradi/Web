using CMS.Domain.Shippings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shippings
{
    public class ShippingRateTranslationConfiguration : IEntityTypeConfiguration<ShippingRateTranslation>
    {
        public void Configure(EntityTypeBuilder<ShippingRateTranslation> builder)
        {
            builder.ToTable("ShippingRateTranslations");

            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);

            // Relations
            builder.HasOne(t => t.ShippingRate)
                   .WithMany(r => r.Translations)
                   .HasForeignKey(t => t.ShippingRateId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(t => t.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Prevent duplicate translations
            builder.HasIndex(t => new { t.ShippingRateId, t.WebSiteLanguageId })
                   .IsUnique();
        }
    }
}
