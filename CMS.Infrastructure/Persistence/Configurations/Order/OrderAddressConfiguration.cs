using CMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Orders
{
    public class OrderAddressConfiguration : IEntityTypeConfiguration<OrderAddress>
    {
        public void Configure(EntityTypeBuilder<OrderAddress> builder)
        {
            builder.ToTable("OrderAddresses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Phone)
                .HasMaxLength(450);

            builder.Property(x => x.ZipCode)
                .HasMaxLength(450);

            // یک OrderAddress چند ترجمه دارد
            builder.HasMany(x => x.Translations)
                .WithOne(x => x.OrderAddress)
                .HasForeignKey(x => x.OrderAddressId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
