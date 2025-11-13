using CMS.Application.Interfaces.Messaging.Notifications;
using CMS.Application.Interfaces.Messaging.Requests;

namespace CMS.Application.Interfaces.Messaging
{
    public interface IAppMediator
    {
        Task<TResponse> Send<TResponse>(IAppRequest<TResponse> request, CancellationToken cancellationToken = default);

        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : IAppNotification;
    }
}
