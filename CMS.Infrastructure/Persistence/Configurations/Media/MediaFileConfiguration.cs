using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CMS.Domain.Media.Entities;

namespace CMS.Infrastructure.Persistence.Configurations.Media
{
    public class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
    {
        public void Configure(EntityTypeBuilder<MediaFile> builder)
        {
            builder.ToTable("MediaFiles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(x => x.FileName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.ContentType)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.MediaType)
                .IsRequired()
                .HasMaxLength(64)
                .HasDefaultValue("Unknown");

            builder.Property(x => x.SizeInBytes).IsRequired();
            builder.Property(x => x.UploadedAt).IsRequired();

            builder.HasIndex(x => x.Key).IsUnique();
        }
    }
}
