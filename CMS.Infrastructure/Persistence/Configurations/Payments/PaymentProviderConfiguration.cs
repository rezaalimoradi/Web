using CMS.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Payments
{
    public class PaymentProviderConfiguration : IEntityTypeConfiguration<PaymentProvider>
    {
        public void Configure(EntityTypeBuilder<PaymentProvider> builder)
        {
            builder.ToTable("PaymentProviders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(x => x.ConfigureUrl)
                   .HasMaxLength(450);

            builder.Property(x => x.LandingViewComponentName)
                   .HasMaxLength(450);

            builder.Property(x => x.AdditionalSettings)
                   .HasColumnType("nvarchar(max)");

            builder.HasMany(x => x.Translations)
                   .WithOne(t => t.PaymentProvider)
                   .HasForeignKey(t => t.PaymentProviderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
