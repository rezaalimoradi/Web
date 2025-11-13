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
    public class CompareListConfiguration : IEntityTypeConfiguration<CompareList>
    {
        public void Configure(EntityTypeBuilder<CompareList> builder)
        {
            builder.ToTable("CompareLists");

            builder.HasKey(cl => cl.Id);

            builder.Property(cl => cl.WebsiteId)
                   .IsRequired();

            builder.Property(cl => cl.CustomerId)
                   .IsRequired();

            // ✅ یک CompareList چند CompareItem دارد
            builder.HasMany(cl => cl.Items)
                   .WithOne(ci => ci.CompareList)
                   .HasForeignKey(ci => ci.CompareListId)
                   .OnDelete(DeleteBehavior.Cascade); // حذف لیست → حذف آیتم‌ها

            // ✅ Index پیشنهادی برای سرعت
            builder.HasIndex(cl => new { cl.CustomerId, cl.WebsiteId });
        }
    }
}
