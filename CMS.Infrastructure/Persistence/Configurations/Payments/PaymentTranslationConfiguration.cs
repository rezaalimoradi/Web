using CMS.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Payments
{
    public class PaymentTranslationConfiguration : IEntityTypeConfiguration<PaymentTranslation>
    {
        public void Configure(EntityTypeBuilder<PaymentTranslation> builder)
        {
            builder.ToTable("PaymentTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PaymentMethod)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(x => x.FailureMessage)
                   .HasMaxLength(1000);

            builder.HasOne(x => x.Payment)
                   .WithMany(p => p.Translations)
                   .HasForeignKey(x => x.PaymentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(x => x.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
