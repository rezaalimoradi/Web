using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CartRuleConfiguration : IEntityTypeConfiguration<CartRule>
    {
        public void Configure(EntityTypeBuilder<CartRule> builder)
        {
            builder.ToTable("CartRules");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(r => r.Description)
                   .HasMaxLength(1000);

            builder.Property(r => r.RuleToApply)
                   .HasMaxLength(500);

            builder.Property(r => r.DiscountAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(r => r.MaxDiscountAmount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(r => r.CreatedOn)
                   .IsRequired();

            builder.Property(r => r.LatestUpdatedOn)
                   .IsRequired();

            // رابطه با ترجمه‌ها
            builder.HasMany(r => r.Translations)
                   .WithOne(t => t.CartRule)
                   .HasForeignKey(t => t.CartRuleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
