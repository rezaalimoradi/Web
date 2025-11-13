using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CartRuleCustomerGroupConfiguration : IEntityTypeConfiguration<CartRuleCustomerGroup>
    {
        public void Configure(EntityTypeBuilder<CartRuleCustomerGroup> builder)
        {
            builder.ToTable("CartRuleCustomerGroups");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.CartRule)
                   .WithMany() // اگه خواستی می‌تونی Collection بزنی: r.CustomerGroups
                   .HasForeignKey(x => x.CartRuleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CustomerGroup)
                   .WithMany()
                   .HasForeignKey(x => x.CustomerGroupId)
                   .OnDelete(DeleteBehavior.Restrict);

            // هر گروه مشتری فقط یکبار می‌تونه به یک CartRule وصل بشه
            builder.HasIndex(x => new { x.CartRuleId, x.CustomerGroupId })
                   .IsUnique();
        }
    }
}
