using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CartRuleUsageConfiguration : IEntityTypeConfiguration<CartRuleUsage>
    {
        public void Configure(EntityTypeBuilder<CartRuleUsage> builder)
        {
            builder.ToTable("CartRuleUsages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UsedOn)
                   .IsRequired();

            // ارتباط با CartRule
            builder.HasOne(x => x.CartRule)
                   .WithMany() // اگر تو CartRule لیست Usages داری، میشه WithMany(r => r.Usages)
                   .HasForeignKey(x => x.CartRuleId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ارتباط با Customer
            builder.HasOne(x => x.Customer)
                   .WithMany()
                   .HasForeignKey(x => x.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            // هر مشتری نباید بتونه دوبار پشت سر هم از یک CartRule استفاده کنه (مگر Rule اجازه بده)
            builder.HasIndex(x => new { x.CartRuleId, x.CustomerId, x.UsedOn });
        }
    }
}
