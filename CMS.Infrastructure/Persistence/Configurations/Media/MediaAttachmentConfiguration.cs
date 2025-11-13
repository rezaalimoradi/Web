using CMS.Domain.Media.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Persistence.Configurations.Media
{
    public class MediaAttachmentConfiguration : IEntityTypeConfiguration<MediaAttachment>
    {
        public void Configure(EntityTypeBuilder<MediaAttachment> builder)
        {
            builder.ToTable("MediaAttachments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EntityType)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.Purpose)
                .HasMaxLength(64);

            builder.Property(x => x.DisplayOrder)
                .HasDefaultValue(0);

            builder.Property(x => x.AttachedAt)
                .IsRequired();

            builder.HasOne(x => x.MediaFile)
                .WithMany()
                .HasForeignKey(x => x.MediaFileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.EntityType, x.EntityId });
            builder.HasIndex(x => new { x.EntityType, x.EntityId, x.Purpose });
        }
    }
}
