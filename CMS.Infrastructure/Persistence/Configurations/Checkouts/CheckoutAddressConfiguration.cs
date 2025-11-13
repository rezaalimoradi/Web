using CMS.Domain.Checkouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Checkouts
{
    public class CheckoutAddressConfiguration : IEntityTypeConfiguration<CheckoutAddress>
    {
        public void Configure(EntityTypeBuilder<CheckoutAddress> builder)
        {
            builder.ToTable("CheckoutAddresses");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.ContactName)
                   .HasMaxLength(450);

            builder.Property(a => a.Phone)
                   .HasMaxLength(450);

            builder.Property(a => a.AddressLine1)
                   .HasMaxLength(450)
                   .IsRequired();

            builder.Property(a => a.AddressLine2)
                   .HasMaxLength(450);

            builder.Property(a => a.City)
                   .HasMaxLength(450)
                   .IsRequired();

            builder.Property(a => a.ZipCode)
                   .HasMaxLength(450);

            // Navigation: Translations
            builder.HasMany(a => a.Translations)
                   .WithOne(t => t.CheckoutAddress)
                   .HasForeignKey(t => t.CheckoutAddressId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
