using CMS.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.ShoppingCarts
{
    public class ShoppingCartTranslationConfiguration : IEntityTypeConfiguration<ShoppingCartTranslation>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartTranslation> builder)
        {
            builder.ToTable("ShoppingCartTranslations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Note)
                   .HasMaxLength(500);

            builder.HasOne(t => t.ShoppingCart)
                   .WithMany(c => c.Translations)
                   .HasForeignKey(t => t.ShoppingCartId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(t => t.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Restrict);

            // یکتا بودن هر زبان برای یک ShoppingCart
            builder.HasIndex(t => new { t.ShoppingCartId, t.WebSiteLanguageId })
                   .IsUnique();
        }
    }
}
