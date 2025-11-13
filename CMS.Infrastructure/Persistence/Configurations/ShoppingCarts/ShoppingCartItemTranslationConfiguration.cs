using CMS.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.ShoppingCarts
{
    public class ShoppingCartItemTranslationConfiguration : IEntityTypeConfiguration<ShoppingCartItemTranslation>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItemTranslation> builder)
        {
            builder.ToTable("ShoppingCartItemTranslations");

            builder.HasKey(t => t.Id);

            // نام و توضیح مطابق با کلاس
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(t => t.Description)
                   .HasMaxLength(500);

            // روابط
            builder.HasOne(t => t.ShoppingCartItem)
                   .WithMany(i => i.Translations) // مطمئن شو ShoppingCartItem دارای ICollection<ShoppingCartItemTranslation> Translations هست
                   .HasForeignKey(t => t.ShoppingCartItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(t => t.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Restrict);

            // یکتا بودن ترجمه برای هر آیتم در هر زبان
            builder.HasIndex(t => new { t.ShoppingCartItemId, t.WebSiteLanguageId })
                   .IsUnique();
        }
    }
}
