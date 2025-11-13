using CMS.Domain.Tenants.Entitis;
using CMS.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Tenants
{
    public class TopLevelDomainConfiguration : IEntityTypeConfiguration<TopLevelDomain>
    {
        public void Configure(EntityTypeBuilder<TopLevelDomain> builder)
        {
            builder.ToTable("TopLevelDomains");

            builder.HasKey(tld => tld.Id);

            builder.Property(tld => tld.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(tld => tld.Extension)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(tld => tld.IsAllowed)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.Property(ws => ws.CreatedAt)
                .IsRequired();
        }
    }
}
