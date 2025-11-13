namespace CMS.Domain.Common
{
    public abstract partial class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void SetUpdatedAt(DateTime? dateTime = null)
        {
            UpdatedAt = dateTime ?? DateTime.UtcNow;
        }
    }

}
