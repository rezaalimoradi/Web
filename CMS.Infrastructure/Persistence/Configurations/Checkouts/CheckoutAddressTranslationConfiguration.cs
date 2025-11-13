using CMS.Domain.Checkouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Checkouts
{
    public class CheckoutAddressTranslationConfiguration : IEntityTypeConfiguration<CheckoutAddressTranslation>
    {
        public void Configure(EntityTypeBuilder<CheckoutAddressTranslation> builder)
        {
            builder.ToTable("CheckoutAddressTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.AddressLine1)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.AddressLine2)
                .HasMaxLength(450);

            builder.Property(x => x.WebSiteLanguageId)
                .IsRequired();

            builder.Property(x => x.CheckoutAddressId)
                .IsRequired();

            builder.HasOne(x => x.CheckoutAddress)
                .WithMany(x => x.Translations)
                .HasForeignKey(x => x.CheckoutAddressId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
