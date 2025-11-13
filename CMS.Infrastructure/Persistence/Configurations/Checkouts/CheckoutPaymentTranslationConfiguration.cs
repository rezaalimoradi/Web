using CMS.Domain.Checkouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Checkouts
{
    public class CheckoutPaymentTranslationConfiguration : IEntityTypeConfiguration<CheckoutPaymentTranslation>
    {
        public void Configure(EntityTypeBuilder<CheckoutPaymentTranslation> builder)
        {
            builder.ToTable("CheckoutPaymentTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Language)
                .IsRequired()
                .HasMaxLength(10); // چون کد زبان دو حرفی (en, fa) است، اینجا تا 10 کاراکتر می‌گذاریم برای انعطاف

            builder.Property(x => x.PaymentMethodName)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.FailureMessage)
                .HasMaxLength(1000);

            builder.HasOne(x => x.CheckoutPayment)
                .WithMany() // اگه داخل CheckoutPayment کلکشن Translations داری باید WithMany(p => p.Translations) باشه
                .HasForeignKey(x => x.CheckoutPaymentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
