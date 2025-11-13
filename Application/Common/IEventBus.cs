using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : INotification;
    }
}
