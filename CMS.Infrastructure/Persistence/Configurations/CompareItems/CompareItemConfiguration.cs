using CMS.Domain.Catalog.Entities;
using CMS.Domain.CompareItems.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Persistence.Configurations.CompareItems
{
    public class CompareItemConfiguration : IEntityTypeConfiguration<CompareItem>
    {
        public void Configure(EntityTypeBuilder<CompareItem> builder)
        {
            builder.ToTable("CompareItems");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.ProductId)
                   .IsRequired();

            // زمان درج
            builder.Property(ci => ci.AddedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ci => ci.CompareListId)
                   .IsRequired();

            // ✅ جلوگیری از ورود آیتم‌های تکراری
            builder.HasIndex(ci => new { ci.ProductId, ci.CompareListId })
                   .IsUnique();

            // ✅ اتصال به Product (در صورت نیاز)
            builder.HasOne(ci => ci.Product)
                   .WithMany()
                   .HasForeignKey(ci => ci.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
