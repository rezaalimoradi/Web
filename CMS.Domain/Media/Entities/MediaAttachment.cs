using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Media.Entities
{
    public class MediaAttachment : BaseEntity
    {
        public long MediaFileId { get; private set; }
        public MediaFile MediaFile { get; set; } = default!;
        public long TenantId { get; private set; }

        public long EntityId { get; private set; }
        public string EntityType { get; private set; } = default!;
        public string? Purpose { get; private set; }
        public int? DisplayOrder { get; private set; } = 0;
        public DateTime AttachedAt { get; private set; } = DateTime.UtcNow;

        protected MediaAttachment() { } // EF Core

        public MediaAttachment(long tenantId,long mediaFileId, long entityId, string entityType, string? purpose = null, int displayOrder = 0)
        {
            if (mediaFileId <= 0) throw new DomainException("Invalid MediaFileId.");
            if (entityId <= 0) throw new DomainException("Invalid EntityId.");
            if (string.IsNullOrWhiteSpace(entityType)) throw new DomainException("EntityType is required.");

            MediaFileId = mediaFileId;
            EntityId = entityId;
            EntityType = entityType.Trim();
            Purpose = string.IsNullOrWhiteSpace(purpose) ? null : purpose.Trim();
            DisplayOrder = displayOrder;
            AttachedAt = DateTime.UtcNow;
            TenantId = tenantId;
        }

        public void SetPurpose(string purpose)
        {
            Purpose = string.IsNullOrWhiteSpace(purpose) ? null : purpose.Trim();
            SetUpdatedAt();
        }

        public void SetDisplayOrder(int order)
        {
            DisplayOrder = order;
            SetUpdatedAt();
        }

        public void ChangeEntity(long entityId, string entityType)
        {
            if (entityId <= 0) throw new DomainException("Invalid EntityId.");
            if (string.IsNullOrWhiteSpace(entityType)) throw new DomainException("EntityType is required.");

            EntityId = entityId;
            EntityType = entityType.Trim();
            SetUpdatedAt();
        }

        public void AttachTo(long entityId, string entityType, string? purpose = null, int displayOrder = 0)
        {
            if (entityId <= 0) throw new DomainException("Invalid EntityId.");
            if (string.IsNullOrWhiteSpace(entityType)) throw new DomainException("EntityType is required.");

            EntityId = entityId;
            EntityType = entityType.Trim();
            Purpose = string.IsNullOrWhiteSpace(purpose) ? Purpose : purpose.Trim();
            DisplayOrder = displayOrder;
            SetUpdatedAt();
        }
    }

}
