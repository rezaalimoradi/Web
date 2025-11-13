using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CartRuleCategoryConfiguration : IEntityTypeConfiguration<CartRuleCategory>
    {
        public void Configure(EntityTypeBuilder<CartRuleCategory> builder)
        {
            builder.ToTable("CartRuleCategories");

            builder.HasKey(cc => cc.Id);

            // روابط
            builder.HasOne(cc => cc.CartRule)
                   .WithMany() // اگه بعداً خواستی Collection بزنی، اینجا تغییر می‌کنه
                   .HasForeignKey(cc => cc.CartRuleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cc => cc.Category)
                   .WithMany()
                   .HasForeignKey(cc => cc.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            // یکتا بودن هر Category روی یک CartRule
            builder.HasIndex(cc => new { cc.CartRuleId, cc.CategoryId })
                   .IsUnique();
        }
    }
}
