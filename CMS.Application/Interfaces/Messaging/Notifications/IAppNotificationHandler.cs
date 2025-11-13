namespace CMS.Application.Interfaces.Messaging.Notifications
{
    public interface IAppNotificationHandler<in TNotification> : MediatR.INotificationHandler<TNotification> where TNotification : IAppNotification
    {
    }
}
