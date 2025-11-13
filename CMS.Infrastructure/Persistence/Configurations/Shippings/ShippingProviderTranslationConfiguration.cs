using CMS.Domain.Shippings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shippings
{
    public class ShippingProviderTranslationConfiguration : IEntityTypeConfiguration<ShippingProviderTranslation>
    {
        public void Configure(EntityTypeBuilder<ShippingProviderTranslation> builder)
        {
            builder.ToTable("ShippingProviderTranslations");

            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);

            // Relations
            builder.HasOne(t => t.ShippingProvider)
                   .WithMany(sp => sp.Translations)
                   .HasForeignKey(t => t.ShippingProviderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(t => t.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Unique index to prevent duplicate translations for same provider + language
            builder.HasIndex(t => new { t.ShippingProviderId, t.WebSiteLanguageId })
                   .IsUnique();
        }
    }
}
