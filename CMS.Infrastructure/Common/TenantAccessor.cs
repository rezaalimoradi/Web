using CMS.Application.Tenants;
using CMS.Application.Tenants.Dtos;

namespace CMS.Infrastructure.Common
{
    public class TenantAccessor : ITenantAccessor
    {
        public WebSiteDto? CurrentTenant { get; set; }
    }
}
