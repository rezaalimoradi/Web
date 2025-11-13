using Microsoft.AspNetCore.Http;
using System;

namespace Web
{
    public class CookieTokenStorage
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CookieName = "AuthToken";

        public CookieTokenStorage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SaveToken(string token)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return;

            context.Response.Cookies.Append(CookieName, token, new CookieOptions
            {
                HttpOnly = true, // امن‌تر، از JS قابل خواندن نیست
                Secure = true,   // فقط HTTPS
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });
        }

        public string? GetToken()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return null;

            context.Request.Cookies.TryGetValue(CookieName, out var token);
            return token;
        }

        public void ClearToken()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return;

            context.Response.Cookies.Delete(CookieName);
        }
    }
}
