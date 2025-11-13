using CMS.Domain.Shippings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shippings
{
    public class ShippingTranslationConfiguration : IEntityTypeConfiguration<ShippingTranslation>
    {
        public void Configure(EntityTypeBuilder<ShippingTranslation> builder)
        {
            builder.ToTable("ShippingTranslations");

            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(t => t.Description)
                   .HasMaxLength(2000);

            // Relationships
            builder.HasOne(t => t.Shipping)
                   .WithMany(s => s.Translations)
                   .HasForeignKey(t => t.ShippingId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(t => t.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Unique constraint per Shipping + Language
            builder.HasIndex(t => new { t.ShippingId, t.WebSiteLanguageId })
                   .IsUnique();
        }
    }
}
