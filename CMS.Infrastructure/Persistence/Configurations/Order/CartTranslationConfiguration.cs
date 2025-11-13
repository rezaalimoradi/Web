using CMS.Domain.Orders.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Order
{
    public class CartTranslationConfiguration : IEntityTypeConfiguration<CartTranslation>
    {
        public void Configure(EntityTypeBuilder<CartTranslation> builder)
        {
            builder.ToTable("CartTranslations");

            // 🔹 کلید اصلی
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                   .ValueGeneratedOnAdd();

            // 🔹 ویژگی‌ها
            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(250); // مطابق Domain validation (Title <= 250)

            builder.Property(t => t.Description)
                   .HasMaxLength(1000); // مطابق Domain validation (Description <= 1000)

            // 🔹 روابط

            // هر Translation متعلق به یک Cart است
            builder.HasOne(t => t.Cart)
                   .WithMany(c => c.Translations)
                   .HasForeignKey(t => t.CartId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            // هر Translation مربوط به یک زبان است
            builder.HasOne(t => t.WebSiteLanguage)
                   .WithMany()
                   .HasForeignKey(t => t.WebSiteLanguageId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // 🔹 ایندکس برای جلوگیری از ترجمه‌ی تکراری در یک زبان برای یک Cart
            builder.HasIndex(t => new { t.CartId, t.WebSiteLanguageId })
                   .IsUnique()
                   .HasDatabaseName("IX_CartTranslations_Cart_Language");

            // 🔹 جلوگیری از بارگذاری خودکار زبان (برای کارایی بهتر)
            builder.Navigation(t => t.WebSiteLanguage).AutoInclude(false);
        }
    }
}
