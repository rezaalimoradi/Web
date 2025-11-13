using CMS.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Users
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Area)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Controller)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Action)
                .IsRequired();

            builder.HasIndex(p => new { p.Area, p.Controller, p.Action })
                .IsUnique();
        }
    }
}
