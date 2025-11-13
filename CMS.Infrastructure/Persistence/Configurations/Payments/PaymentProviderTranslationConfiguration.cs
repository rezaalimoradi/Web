using CMS.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Payments
{
    public class PaymentProviderTranslationConfiguration : IEntityTypeConfiguration<PaymentProviderTranslation>
    {
        public void Configure(EntityTypeBuilder<PaymentProviderTranslation> builder)
        {
            builder.ToTable("PaymentProviderTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(450)
                   .IsRequired(false);

            builder.Property(x => x.ConfigureUrl)
                   .HasMaxLength(450)
                   .IsRequired(false);

            builder.Property(x => x.LandingViewComponentName)
                   .HasMaxLength(450)
                   .IsRequired(false);

            builder.HasOne(x => x.PaymentProvider)
                   .WithMany(p => p.Translations)
                   .HasForeignKey(x => x.PaymentProviderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(x => x.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
