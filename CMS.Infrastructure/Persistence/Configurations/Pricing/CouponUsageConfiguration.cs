using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CouponUsageConfiguration : IEntityTypeConfiguration<CouponUsage>
    {
        public void Configure(EntityTypeBuilder<CouponUsage> builder)
        {
            builder.ToTable("CouponUsages");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UsedOn)
                   .IsRequired();

            // ارتباط با Coupon
            builder.HasOne(u => u.Coupon)
                   .WithMany(c => c.Usages)
                   .HasForeignKey(u => u.CouponId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ارتباط با Customer (AppUser)
            builder.HasOne(u => u.Customer)
                   .WithMany()
                   .HasForeignKey(u => u.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            // هر مشتری نمی‌تواند یک کوپن خاص را دوبار استفاده کند
            builder.HasIndex(u => new { u.CouponId, u.CustomerId })
                   .IsUnique();
        }
    }
}
