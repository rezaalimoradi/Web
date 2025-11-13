using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Order
{
    public class CartItemTranslationConfiguration : IEntityTypeConfiguration<CartItemTranslation>
    {
        public void Configure(EntityTypeBuilder<CartItemTranslation> builder)
        {
            builder.ToTable("CartItemTranslations");

            // 🔹 کلید اصلی
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                   .ValueGeneratedOnAdd();

            // 🔹 ویژگی‌ها
            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(250); // مطابق Domain validation (Title <= 250)

            // 🔹 روابط

            // هر Translation متعلق به یک CartItem است
            builder.HasOne(t => t.CartItem)
                   .WithMany(ci => ci.Translations)
                   .HasForeignKey(t => t.CartItemId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            // هر Translation مربوط به یک WebSiteLanguage است
            builder.HasOne(t => t.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(t => t.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // 🔹 ایندکس برای جلوگیری از ترجمه تکراری در یک زبان برای یک CartItem
            builder.HasIndex(t => new { t.CartItemId, t.WebSiteLanguageId })
                   .IsUnique()
                   .HasDatabaseName("IX_CartItemTranslations_CartItem_Language");

            // 🔹 جلوگیری از بارگذاری خودکار زبان (برای کارایی بهتر)
            builder.Navigation(t => t.WebSiteLanguage).AutoInclude(false);
        }
    }
}
