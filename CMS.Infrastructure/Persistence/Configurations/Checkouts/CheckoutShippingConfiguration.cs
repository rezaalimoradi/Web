using CMS.Domain.Checkouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Checkouts
{
    public class CheckoutShippingConfiguration : IEntityTypeConfiguration<CheckoutShipping>
    {
        public void Configure(EntityTypeBuilder<CheckoutShipping> builder)
        {
            builder.ToTable("CheckoutShippings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Method)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.IsEnabled)
                .IsRequired();

            builder.HasOne(x => x.Checkout)
                .WithMany() // اگه در Checkout لیست Shippings داری، باید WithMany(c => c.Shippings) باشه
                .HasForeignKey(x => x.CheckoutId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
