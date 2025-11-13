using CMS.Application.Interfaces.Messaging;
using CMS.Application.Interfaces.Messaging.Notifications;
using CMS.Application.Interfaces.Messaging.Requests;
using MediatR;

namespace CMS.Infrastructure.Messaging
{
    public class AppMediator : IAppMediator
    {
        private readonly IMediator _mediator;

        public AppMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<TResponse> Send<TResponse>(IAppRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(request, cancellationToken);
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : IAppNotification
        {
            return _mediator.Publish(notification, cancellationToken);
        }
    }
}
