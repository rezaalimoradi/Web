using CMS.Domain.Tenants.Entitis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Tenants
{
    public class WebSiteDomainConfiguration : IEntityTypeConfiguration<WebSiteDomain>
    {
        public void Configure(EntityTypeBuilder<WebSiteDomain> builder)
        {
            builder.ToTable("WebSiteDomains");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(d => d.DomainName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(d => d.IsDefault)
                   .IsRequired();

            builder.HasOne(d => d.Tld)
                   .WithMany(t => t.Domains)
                   .HasForeignKey(d => d.TldId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ws => ws.CreatedAt)
                .IsRequired();

            builder.HasIndex(d => new { d.DomainName, d.TldId, d.WebSiteId })
                .IsUnique();
        }
    }
}
