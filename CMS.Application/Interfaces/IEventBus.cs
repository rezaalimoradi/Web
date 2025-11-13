using MediatR;

namespace CMS.Application.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : INotification;
    }
}
