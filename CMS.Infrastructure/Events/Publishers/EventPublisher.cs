using CMS.Application.Interfaces;
using MediatR;

namespace CMS.Infrastructure.Events.Publishers
{
    public class EventPublisher : IEventBus
    {
        private readonly IMediator _mediator;

        public EventPublisher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishAsync<T>(T @event) where T : INotification
        {
            await _mediator.Publish(@event);
        }
    }
}
