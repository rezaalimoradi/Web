using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CatalogRuleCustomerGroupConfiguration : IEntityTypeConfiguration<CatalogRuleCustomerGroup>
    {
        public void Configure(EntityTypeBuilder<CatalogRuleCustomerGroup> builder)
        {
            builder.ToTable("CatalogRuleCustomerGroups");

            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.CatalogRule)
                   .WithMany()
                   .HasForeignKey(c => c.CatalogRuleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.CustomerGroup)
                   .WithMany()
                   .HasForeignKey(c => c.CustomerGroupId)
                   .OnDelete(DeleteBehavior.Restrict);

            // جلوگیری از ثبت تکراری برای یک CatalogRule و یک CustomerGroup
            builder.HasIndex(c => new { c.CatalogRuleId, c.CustomerGroupId })
                   .IsUnique();
        }
    }
}
