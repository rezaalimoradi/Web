using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CouponTranslationConfiguration : IEntityTypeConfiguration<CouponTranslation>
    {
        public void Configure(EntityTypeBuilder<CouponTranslation> builder)
        {
            builder.ToTable("CouponTranslations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Culture)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(t => t.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(t => t.Description)
                   .HasMaxLength(500);

            builder.HasOne(t => t.Coupon)
                   .WithMany(c => c.Translations)
                   .HasForeignKey(t => t.CouponId)
                   .OnDelete(DeleteBehavior.Cascade);

            // جلوگیری از ثبت ترجمه تکراری برای یک زبان روی یک کوپن
            builder.HasIndex(t => new { t.CouponId, t.Culture })
                   .IsUnique();
        }
    }
}
