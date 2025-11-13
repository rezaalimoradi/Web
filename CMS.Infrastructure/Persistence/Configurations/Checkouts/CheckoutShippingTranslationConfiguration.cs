using CMS.Domain.Checkouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Checkouts
{
    public class CheckoutShippingTranslationConfiguration : IEntityTypeConfiguration<CheckoutShippingTranslation>
    {
        public void Configure(EntityTypeBuilder<CheckoutShippingTranslation> builder)
        {
            builder.ToTable("CheckoutShippingTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Language)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.DisplayName)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.HasOne(x => x.CheckoutShipping)
                .WithMany() // اگر در CheckoutShipping یک کلکشن Translations اضافه کردی، اینجا باید WithMany(c => c.Translations) باشه
                .HasForeignKey(x => x.CheckoutShippingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
