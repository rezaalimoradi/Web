using CMS.Application.Interfaces.Messaging.Requests;

namespace CMS.Application.Tenants.Commands
{
    public class DeleteWebSiteCommand : IAppRequest<bool>
    {
        public long WebSiteId { get; set; }
    }
}
