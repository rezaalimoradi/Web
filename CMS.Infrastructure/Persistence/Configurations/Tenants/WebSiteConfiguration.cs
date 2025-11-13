using CMS.Domain.Tenants.Entitis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Tenants
{
    public class WebSiteConfiguration : IEntityTypeConfiguration<WebSite>
    {
        public void Configure(EntityTypeBuilder<WebSite> builder)
        {
            builder.ToTable("WebSites");

            builder.HasKey(ws => ws.Id);

            builder.Property(ws => ws.Id)
                .ValueGeneratedOnAdd();

            builder.OwnsOne(ws => ws.ContactInfo, contactInfo =>
            {
                contactInfo.Property(ci => ci.Email)
                    .IsRequired(false)
                    .HasMaxLength(255);

                contactInfo.Property(ci => ci.PhoneNumber)
                    .IsRequired(false)
                    .HasMaxLength(50);

                contactInfo.Property(ci => ci.Address)
                .IsRequired(false)
                    .HasMaxLength(500);
            });

            builder.Property(ws => ws.CompanyName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(ws => ws.LogoUrl)
                .HasMaxLength(2083);

            builder.Property(ws => ws.IsActive)
                .IsRequired();

            builder.Property(ws => ws.Description)
                .HasMaxLength(2000);

            builder.Property(ws => ws.CreatedAt)
                .IsRequired();

            builder.HasOne(w=> w.Owner)
                .WithMany()
                .HasForeignKey(w=> w.OwnerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(w => w.Domains)
                .WithOne(d => d.WebSite)
                .HasForeignKey(d => d.WebSiteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(w => w.SupportedLanguages)
                .WithOne(l => l.WebSite)
                .HasForeignKey(l => l.WebSiteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ws => ws.CompanyName)
                .HasDatabaseName("IX_WebSites_CompanyName")
                .IsUnique(false);

        }
    }
}
