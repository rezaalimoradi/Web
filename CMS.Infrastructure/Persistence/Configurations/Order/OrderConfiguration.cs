using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<CMS.Domain.Orders.Order>
    {
        public void Configure(EntityTypeBuilder<CMS.Domain.Orders.Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CouponCode)
                .HasMaxLength(450);

            builder.Property(x => x.CouponRuleName)
                .HasMaxLength(450);

            // روابط با User ها
            builder.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.LatestUpdatedBy)
                .WithMany()
                .HasForeignKey(x => x.LatestUpdatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false); // چون ممکنه هنوز آپدیتی انجام نشده باشد

            // روابط با آدرس‌ها
            builder.HasOne(x => x.ShippingAddress)
                .WithMany()
                .HasForeignKey(x => x.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BillingAddress)
                .WithMany()
                .HasForeignKey(x => x.BillingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // رابطه Parent/Children
            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItems
            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Translations
            builder.HasMany(x => x.Translations)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Histories
            builder.HasMany(x => x.Histories)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
