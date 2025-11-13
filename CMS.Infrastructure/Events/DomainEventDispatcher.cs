using CMS.Application.Interfaces;
using CMS.Domain.Common;
using MediatR;

namespace CMS.Infrastructure.Events
{
    public class DomainEventDispatcher
    {
        private readonly IEventBus _eventBus;

        public DomainEventDispatcher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task DispatchEventsAsync(AggregateRoot root)
        {
            foreach (var @event in root.DomainEvents)
            {
                if (@event is INotification notification)
                {
                    await _eventBus.PublishAsync(notification);
                }
                else
                {
                    throw new InvalidOperationException("Domain event must implement INotification.");
                }
            }

            root.ClearDomainEvents();
        }

    }
}
