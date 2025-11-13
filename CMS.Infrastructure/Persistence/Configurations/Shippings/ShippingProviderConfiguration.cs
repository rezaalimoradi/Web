using CMS.Domain.Shippings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Shippings
{
    public class ShippingProviderConfiguration : IEntityTypeConfiguration<ShippingProvider>
    {
        public void Configure(EntityTypeBuilder<ShippingProvider> builder)
        {
            builder.ToTable("ShippingProviders");

            builder.HasKey(sp => sp.Id);

            // Properties
            builder.Property(sp => sp.ConfigureUrl)
                   .HasMaxLength(1000);

            builder.Property(sp => sp.LandingViewComponentName)
                   .HasMaxLength(450);

            builder.Property(sp => sp.AdditionalSettings)
                   .HasColumnType("nvarchar(max)");

            builder.Property(sp => sp.IsEnabled)
                   .IsRequired();

            builder.Property(sp => sp.CreatedOn)
                   .IsRequired();

            builder.Property(sp => sp.LatestUpdatedOn)
                   .IsRequired();

            // Relations
            builder.HasMany(sp => sp.Translations)
                   .WithOne(t => t.ShippingProvider)
                   .HasForeignKey(t => t.ShippingProviderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
