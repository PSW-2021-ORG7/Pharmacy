using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace backend.RabbitMqServices
{
    public class TenderingRequestService : BackgroundService
    {
        IConnection connection;
        IModel channel;
        private CancellationToken cancellationToken;

        public override Task StartAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "tendering-queue",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var jsonMessage = Encoding.UTF8.GetString(body);
                String message;
                try
                {   // try deserialize with default datetime format
                    message = JsonConvert.DeserializeObject<String>(jsonMessage);
                }
                catch (Exception)     // datetime format not default, deserialize with Java format (milliseconds since 1970/01/01)
                {
                    message = JsonConvert.DeserializeObject<String>(jsonMessage);
                }

                String response = message;

            };
            channel.BasicConsume(queue: "tendering-queue",
                                    autoAck: true,
                                    consumer: consumer);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            channel.Close();
            connection.Close();
            return base.StopAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

    }
}
