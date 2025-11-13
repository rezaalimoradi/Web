namespace CMS.Application.Tenants
{
    public interface ITenantContext
    {
        long TenantId { get; }

        string Theme { get; }

        long CurrentLanguageId { get; }

        /// <summary>
        /// شناسه مشتری فعلی (کاربر لاگین‌شده یا مهمان)
        /// </summary>
        string CustomerIdentifier { get; }

        /// <summary>
        /// شناسه کاربر لاگین‌شده، اگر کاربر مهمان است null برمی‌گرداند
        /// </summary>
        long? UserId { get; }
    }
}
