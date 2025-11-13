using CMS.Application.Interfaces.Messaging.Notifications;
using CMS.Application.Tenants.Events;

namespace CMS.Application.Tenants.Consumers
{
    public class TenantCreatedEventConsumer : IAppNotificationHandler<WebSiteCreatedEvent>
    {
        public async Task Handle(WebSiteCreatedEvent notification, CancellationToken cancellationToken)
        {
            //Do somethings...

            await Task.CompletedTask;
        }
    }
}
