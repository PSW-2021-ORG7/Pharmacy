using backend.Model;
using backend.Repositories;
using backend.Repositories.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services
{
    public class MedicineInventoryService
    {
        IMedicineInventoryRepository medicineInventoryRepository;
        private MedicineService medicineService = new MedicineService(new MedicineRepository(new DAL.DrugStoreContext()), new MedicineInventoryRepository(new DAL.DrugStoreContext()));
        public MedicineInventoryService(IMedicineInventoryRepository medicineInventoryRepository)
        {
            this.medicineInventoryRepository = medicineInventoryRepository;
        }

        public void Save(MedicineInventory medicineInventory)
        {
            medicineInventoryRepository.Save(medicineInventory);
        }

        public bool Update(MedicineInventory medicineInventory)
        {
            if (medicineInventoryRepository.Update(medicineInventory)) return true;
            return false;
        }

        public bool ReduceMedicineQuantity(MedicineInventory medicineInventory)
        {
            //bool updated = medicineInventoryRepository.ReduceMedicineQuantity(medicineInventory);
            MedicineInventory medicine = null;
            foreach (MedicineInventory changedMedicine in medicineInventoryRepository.GetAll())
                if (changedMedicine.MedicineId.Equals(medicineInventory.MedicineId))
                {
                    medicine = changedMedicine;
                    break;
                }
         
            if (medicine != null)
            {
                medicine.Quantity -= medicineInventory.Quantity;
                if (medicine.Quantity < 0) return false;
                medicineInventoryRepository.Update(medicine);
                return true;
            }
            return false;
        }

        public List<MedicineInventory> UpdateMultipleMedicines(List<MedicineInventory> medicines)
        {
            List<MedicineInventory> medicinesUnableToUpdate = new List<MedicineInventory>();
            foreach (MedicineInventory medicine in medicines)
            {
                if (!medicineInventoryRepository.Update(medicine)) medicinesUnableToUpdate.Add(medicine);
            }
            return medicinesUnableToUpdate;
        }


        public bool DeleteMedicineInventory(MedicineInventory medicineInventory)
        {
            medicineInventoryRepository.Delete(medicineInventory);
            return true; //napomena!
		}

        public List<MedicineInventory> GetAll()
        {
            return medicineInventoryRepository.GetAll();
        }

        internal object UpdateMedicinePrice(MedicineInventory medicineInventory)
        {
            if (medicineInventory.Price >= 0)
            {
                return medicineInventoryRepository.Update(medicineInventory);
            }
            return false;
        }

        public bool SendEmail(MedicineInventory inventory)
        {
            String content = CreateBody(inventory);
            EmailMessage email = new EmailMessage("pswintegrationtesting@gmail.com", "Delivery Details", content);
            EmailConfiguration config = new EmailConfiguration();
            MimeMessage emailToSend = CreateEmailMessage(email, config);

            Send(emailToSend, config);
            return true;
        }

        private string CreateBody(MedicineInventory inventory)
        {
            Medicine medicine = medicineService.GetByID(inventory.MedicineId);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Thank you for ordering!")
                    .AppendLine()
                    .AppendLine("Delivery details:")
                    .AppendLine()
                    .AppendLine("Medicine: " + medicine.Name + " " + medicine.DosageInMilligrams + "mg")
                    .AppendLine("Quantity: " + inventory.Quantity);

            return builder.ToString();
        }

        private MimeMessage CreateEmailMessage(EmailMessage message, EmailConfiguration config)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("AdminPharmacy", config.From));
            emailMessage.To.Add(new MailboxAddress("User", config.From));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage, EmailConfiguration emailConfig)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    CheckConnection(client, emailConfig, mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();

                }
            }
        }

        private void CheckConnection(SmtpClient client, EmailConfiguration _emailConfig, MimeMessage mailMessage)
        {
            client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
            client.Send(mailMessage);
        }
    }
}
