<<<<<<< HEAD
﻿using AutoMapper;
using backend.DAL;
using backend.DTO;
using backend.DTO.TenderingDTO;
using backend.Model;
using backend.Repositories;
using backend.Services;
using Microsoft.Extensions.Hosting;
=======
﻿using Microsoft.Extensions.Hosting;
>>>>>>> 2b2804e (feat: Implementation of RabbitMQ consumer service)
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
<<<<<<< HEAD
        private const String apiKey = "XYZX";
        private TenderingService _tenderService = new TenderingService(new MedicineInventoryRepository(new DrugStoreContext()), new MedicineRepository(new DrugStoreContext()));

=======
>>>>>>> 2b2804e (feat: Implementation of RabbitMQ consumer service)
        IConnection connection;
        IModel channel;
        private CancellationToken cancellationToken;

        public override Task StartAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
<<<<<<< HEAD
            channel.QueueDeclare(queue: "tendering-requests-queue",
=======
            channel.QueueDeclare(queue: "tendering-queue",
>>>>>>> 2b2804e (feat: Implementation of RabbitMQ consumer service)
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
<<<<<<< HEAD
                var jsonBody = Encoding.UTF8.GetString(body);
                TenderingRequestDTO tenderingRequest = new TenderingRequestDTO();
                try
                {   // try deserialize with default datetime format
                    tenderingRequest = JsonConvert.DeserializeObject<TenderingRequestDTO>(jsonBody);
                }
                catch (Exception)     
                {
                    tenderingRequest = JsonConvert.DeserializeObject<TenderingRequestDTO>(jsonBody);
                }

                TenderingOffer offer = _tenderService.RequestTenderOfffer(tenderingRequest);      
                TenderingOfferDTO offerToSend = Mapping.Mapper.Map<TenderingOfferDTO>(offer);
                offerToSend.ApiKey = apiKey;
                offerToSend.TenderKey = tenderingRequest.TenderKey;


                _tenderService.sendOfferToHospital(offerToSend); //RabbitMQ

            };
            channel.BasicConsume(queue: "tendering-requests-queue",
=======
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
>>>>>>> 2b2804e (feat: Implementation of RabbitMQ consumer service)
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
