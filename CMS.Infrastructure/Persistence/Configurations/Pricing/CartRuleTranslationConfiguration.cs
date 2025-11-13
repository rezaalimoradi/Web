using CMS.Domain.Pricing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Pricing
{
    public class CartRuleTranslationConfiguration : IEntityTypeConfiguration<CartRuleTranslation>
    {
        public void Configure(EntityTypeBuilder<CartRuleTranslation> builder)
        {
            builder.ToTable("CartRuleTranslations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Culture)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);

            // روابط
            builder.HasOne(t => t.CartRule)
                   .WithMany(r => r.Translations)
                   .HasForeignKey(t => t.CartRuleId)
                   .OnDelete(DeleteBehavior.Cascade);

            // یکتا بودن Culture روی هر CartRule
            builder.HasIndex(t => new { t.CartRuleId, t.Culture })
                   .IsUnique();
        }
    }
}
