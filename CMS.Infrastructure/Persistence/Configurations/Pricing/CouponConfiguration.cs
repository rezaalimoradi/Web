using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Coupons");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasIndex(c => c.Code)
                   .IsUnique();

            builder.HasOne(c => c.CartRule)
                   .WithMany()
                   .HasForeignKey(c => c.CartRuleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.IsActive)
                   .IsRequired();

            builder.Property(c => c.ExpiresOn)
                   .IsRequired(false);

            builder.HasMany(c => c.Translations)
                   .WithOne(t => t.Coupon)
                   .HasForeignKey(t => t.CouponId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Usages)
                   .WithOne(u => u.Coupon)
                   .HasForeignKey(u => u.CouponId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
