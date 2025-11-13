using CMS.Application.Tenants.Dtos;

namespace CMS.Application.Tenants
{
    public interface ITenantAccessor
    {
        WebSiteDto? CurrentTenant { get; set; }
    }
}
