using CMS.Application.Interfaces;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CMS.Infrastructure.Events.Publishers
{
    public class RabbitMqEventBus : IEventBus
    {
        private IConnection _connection;
        private IChannel _channel;

        public RabbitMqEventBus()
        {
            _connection = null!;
            _channel = null!;
        }

        public async Task InitializeAsync()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
        }

        public async Task PublishAsync<T>(T @event) where T : INotification
        {
            if (_channel == null)
            {
                throw new InvalidOperationException("RabbitMQ Channel Was Not Created.");
            }

            var queueName = @event.GetType().Name;
            await _channel.QueueDeclareAsync(queueName, false, false, false, null);

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            var props = new BasicProperties();
            props.Persistent = true;

            await _channel.BasicPublishAsync(
               exchange: "",
               routingKey: queueName,
               mandatory: false,
               basicProperties: props,
               body: body,
               cancellationToken: CancellationToken.None
           );

            await Task.CompletedTask;
        }
    }
}
