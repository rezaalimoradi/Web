using CMS.Application.Tenants;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

public class TenantContext : ITenantContext
{
    private readonly ITenantAccessor _tenantAccessor;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantContext(ITenantAccessor tenantAccessor, IHttpContextAccessor httpContextAccessor)
    {
        _tenantAccessor = tenantAccessor ?? throw new ArgumentNullException(nameof(tenantAccessor));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    /// <summary>
    /// شناسه Tenant فعلی (وب‌سایت فعال)
    /// </summary>
    public long TenantId => _tenantAccessor.CurrentTenant?.Id ?? 0;

    /// <summary>
    /// تم فعال سایت (اگر مقداردهی نشده باشد، Default برمی‌گردد)
    /// </summary>
    public string Theme => _tenantAccessor.CurrentTenant?.Theme ?? "Default";

    /// <summary>
    /// زبان فعال سایت
    /// </summary>
    public long CurrentLanguageId => 1;

    /// <summary>
    /// شناسه کاربر لاگین‌شده (اگر لاگین نشده باشد null)
    /// </summary>
    public long? UserId
    {
        get
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                var userIdStr = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (long.TryParse(userIdStr, out var userId))
                    return userId;
            }
            return null; // کاربر مهمان
        }
    }


    /// <summary>
    /// شناسه مشتری فعلی (کاربر لاگین‌شده یا مهمان)
    /// </summary>
    public string CustomerIdentifier
    {
        get
        {
            var userId = UserId;
            if (userId.HasValue)
                return userId.Value.ToString(); 

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return "Anonymous";

            const string cookieName = "CustomerIdentifier";

            if (httpContext.Request.Cookies.TryGetValue(cookieName, out var cookieValue)
                && !string.IsNullOrWhiteSpace(cookieValue))
            {
                return cookieValue;
            }

            // اگر کوکی وجود ندارد، بساز و ذخیره کن
            var newIdentifier = Guid.NewGuid().ToString("N"); // N = بدون dash
            httpContext.Response.Cookies.Append(cookieName, newIdentifier, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                HttpOnly = true,
                IsEssential = true,
                Secure = httpContext.Request.IsHttps
            });

            return newIdentifier;
        }
    }

}
