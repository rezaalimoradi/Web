using CMS.Application.Interfaces.Messaging.Notifications;
using CMS.Domain.Tenants.Entitis;

namespace CMS.Application.Tenants.Events
{
    public class WebSiteLanguageCreatedEvent : IAppNotification
    {
        public WebSiteLanguage WebSiteLanguage{ get; set; }

        public WebSiteLanguageCreatedEvent(WebSiteLanguage webSiteLanguage)
        {
            WebSiteLanguage = webSiteLanguage;
        }
    }
}
