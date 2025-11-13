using CMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Orders
{
    public class OrderAddressTranslationConfiguration : IEntityTypeConfiguration<OrderAddressTranslation>
    {
        public void Configure(EntityTypeBuilder<OrderAddressTranslation> builder)
        {
            builder.ToTable("OrderAddressTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ContactName)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.AddressLine1)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.AddressLine2)
                .HasMaxLength(450);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(450);

            builder.HasOne(x => x.OrderAddress)
                .WithMany(x => x.Translations)
                .HasForeignKey(x => x.OrderAddressId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WebSiteLanguage)
                .WithMany()
                .HasForeignKey(x => x.WebSiteLanguageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
