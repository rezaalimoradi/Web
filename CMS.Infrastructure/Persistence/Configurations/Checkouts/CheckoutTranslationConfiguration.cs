using CMS.Domain.Checkouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Checkouts
{
    public class CheckoutTranslationConfiguration : IEntityTypeConfiguration<CheckoutTranslation>
    {
        public void Configure(EntityTypeBuilder<CheckoutTranslation> builder)
        {
            builder.ToTable("CheckoutTranslations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.OrderNote)
                   .HasMaxLength(1000);

            builder.Property(t => t.ShippingMethod)
                   .HasMaxLength(450);

            builder.Property(t => t.PaymentMethod)
                   .HasMaxLength(450);

            builder.HasIndex(t => new { t.CheckoutId, t.WebSiteLanguageId })
                   .IsUnique(); // یک ترجمه برای هر زبان
        }
    }
}
