using CMS.Domain.Common;
using CMS.Domain.Common.Exceptions;

namespace CMS.Domain.Media.Entities
{
    public class MediaFile : AggregateRoot
    {
        public long TenantId { get; private set; }
        public long? LanguageId { get; private set; }

        public string Key { get; private set; } = default!;
        public string FileName { get; private set; } = default!;
        public string ContentType { get; private set; } = default!;
        public long SizeInBytes { get; private set; }
        public DateTime UploadedAt { get; private set; } = DateTime.UtcNow;
        public string MediaType { get; private set; } = "Unknown";

        protected MediaFile() { }

        public MediaFile(string key, string fileName, string contentType, long sizeInBytes, string mediaType, long tenantId, long languageId)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new DomainException("Media key is required.");
            if (string.IsNullOrWhiteSpace(fileName)) throw new DomainException("FileName is required.");
            if (sizeInBytes < 0) throw new DomainException("Size cannot be negative.");
            if (string.IsNullOrWhiteSpace(contentType)) contentType = "application/octet-stream";

            Key = key.Trim();
            FileName = fileName.Trim();
            ContentType = contentType.Trim();
            SizeInBytes = sizeInBytes;
            MediaType = string.IsNullOrWhiteSpace(mediaType) ? "Unknown" : mediaType.Trim();
            UploadedAt = DateTime.UtcNow;
            TenantId = tenantId;
            LanguageId = languageId;
        }

        public void UpdateKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new DomainException("Media key is required.");
            Key = key.Trim();
            SetUpdatedAt();
        }

        public void UpdateFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new DomainException("FileName is required.");
            FileName = fileName.Trim();
            SetUpdatedAt();
        }
    }

}
