using backend.DAL;
using backend.DTO.TenderingDTO;
using backend.Model;
using backend.Repositories;
using backend.Services;
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
        private TenderingService _tenderService = new TenderingService(new MedicineInventoryRepository(new DrugStoreContext()), new MedicineRepository(new DrugStoreContext()));

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
                var jsonBody = Encoding.UTF8.GetString(body);
                TenderingRequestDTO tenderingRequest = new TenderingRequestDTO();
                try
                {   // try deserialize with default datetime format
                    tenderingRequest = JsonConvert.DeserializeObject<TenderingRequestDTO>(jsonBody);
                }
                catch (Exception)     // datetime format not default, deserialize with Java format (milliseconds since 1970/01/01)
                {
                    tenderingRequest = JsonConvert.DeserializeObject<TenderingRequestDTO>(jsonBody);
                }

                TenderingOffer offer = _tenderService.RequestTenderOfffer(tenderingRequest);
                _tenderService.sendOfferToHospital(offer); //RabbitMQ

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
