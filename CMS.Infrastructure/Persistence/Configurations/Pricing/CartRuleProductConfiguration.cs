using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CartRuleProductConfiguration : IEntityTypeConfiguration<CartRuleProduct>
    {
        public void Configure(EntityTypeBuilder<CartRuleProduct> builder)
        {
            builder.ToTable("CartRuleProducts");

            builder.HasKey(cp => cp.Id);

            // روابط
            builder.HasOne(cp => cp.CartRule)
                   .WithMany()
                   .HasForeignKey(cp => cp.CartRuleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cp => cp.Product)
                   .WithMany()
                   .HasForeignKey(cp => cp.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            // یکتا بودن هر Product روی یک CartRule
            builder.HasIndex(cp => new { cp.CartRuleId, cp.ProductId })
                   .IsUnique();
        }
    }
}
