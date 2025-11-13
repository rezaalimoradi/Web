using CMS.Domain.Shippings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shippings
{
    public class ShippingMethodTranslationConfiguration : IEntityTypeConfiguration<ShippingMethodTranslation>
    {
        public void Configure(EntityTypeBuilder<ShippingMethodTranslation> builder)
        {
            builder.ToTable("ShippingMethodTranslations");

            builder.HasKey(t => t.Id);

            // Relation → ShippingMethod
            builder.HasOne(t => t.ShippingMethod)
                   .WithMany(m => m.Translations)
                   .HasForeignKey(t => t.ShippingMethodId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relation → WebSiteLanguage
            builder.HasOne(t => t.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(t => t.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Properties
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);
        }
    }
}
