using CMS.Application.Interfaces.Messaging.Notifications;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Application.Tenants.Events
{
    public class WebSiteCreatedEvent : IAppNotification
    {
        public WebSite WebSite { get; set; }

        public WebSiteCreatedEvent(WebSite webSite)
        {
            WebSite = webSite;
        }
    }
}
