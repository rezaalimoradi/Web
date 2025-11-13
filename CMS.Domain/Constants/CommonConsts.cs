
namespace CMS.Domain.Constants
{
    public static class CommonConsts
    {
        public static string UsernameSeprator = "{{-}}";

        public static string JwtClaim_Permissions = "permissions";

        public const string SiteId = "site_id";

        public static string GeneratePermissionKey(string area, string controller, string action)
        {
            return $"{area}:{controller}:{action}";
        }
    }
}
