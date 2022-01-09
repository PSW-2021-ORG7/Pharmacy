using AutoMapper;
using backend.DTO;
using backend.DTO.TenderingDTO;
using backend.Model;
using backend.Repositories.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;


namespace backend.Services
{
    public class TenderingService
    {
        private IMedicineInventoryRepository medicineInventoryRepository;
        private IMedicineRepository medicineRepository;

        public TenderingService(IMedicineInventoryRepository mir, IMedicineRepository mr)
        {
            this.medicineInventoryRepository = mir;
            this.medicineRepository = mr;
        }

        public TenderingOffer RequestTenderOfffer(TenderingRequestDTO tenderingRequestDTO)
        {
            TenderingOffer tenderingOffer = new TenderingOffer();

            foreach(TenderingItemRequestDTO requestedItem in tenderingRequestDTO.requestedItems)
            {
                MedicineInventory medicineInventory = medicineInventoryRepository.FindRequestedMedicineInventory(requestedItem);
                TenderingOfferItem newTenderingOfferItem =  new TenderingOfferItem();
                if(medicineInventory != null)
                {
                    newTenderingOfferItem.Medicine = medicineRepository.GetByID(medicineInventory.MedicineId);
                    newTenderingOfferItem.PriceForSingleEntity = medicineInventory.Price;
                    if (requestedItem.RequiredQuantity <= medicineInventory.Quantity)
                    {
                        newTenderingOfferItem.AvailableQuantity = requestedItem.RequiredQuantity;
                        newTenderingOfferItem.MissingQuantity = 0;
                    }
                    else
                    {
                        newTenderingOfferItem.AvailableQuantity = medicineInventory.Quantity;
                        newTenderingOfferItem.MissingQuantity = requestedItem.RequiredQuantity - medicineInventory.Quantity;
                    }
                    
                }
                else
                {
                    newTenderingOfferItem.Medicine = new Medicine(requestedItem.MedicineName, requestedItem.DosageInMilligrams, requestedItem.Manufacturer);
                    newTenderingOfferItem.MissingQuantity = requestedItem.RequiredQuantity;
                    newTenderingOfferItem.PriceForSingleEntity = 0;
                    newTenderingOfferItem.AvailableQuantity = 0;
                }
                tenderingOffer.tenderingOfferItems.Add(newTenderingOfferItem);



            }

            return tenderingOffer;
        }

        public void sendOfferToHospital(TenderingOfferDTO offer)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "tendering-offers-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                TenderingOfferDTO offerToSend = offer;
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(offerToSend));

                channel.BasicPublish(exchange: "",
                                     routingKey: "tendering-offers-queue",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
