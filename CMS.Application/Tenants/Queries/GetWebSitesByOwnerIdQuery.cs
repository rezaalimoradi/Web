using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Tenants.Dtos;

namespace CMS.Application.Tenants.Queries
{
    public class GetWebSitesByOwnerIdQuery : IAppRequest<List<WebSiteDto>>
    {
        public long OwnerId { get; set; }

        public GetWebSitesByOwnerIdQuery(long ownerId)
        {
            OwnerId = ownerId;
        }
    }
}
