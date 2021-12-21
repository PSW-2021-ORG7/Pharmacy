using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model
{
    public class MedicineInventory
    {
        public MedicineInventory(int medicineId)
        {
            this.Price = 0;
            MedicineId = medicineId;
            Quantity = 0;
        }
        public MedicineInventory(int medicineId, double price)
        {
            this.Price = price;
            MedicineId = medicineId;
            Quantity = 0;
        }

        [JsonConstructor]
        public MedicineInventory(int medicineId,int quantity, double price)
        {
            MedicineId = medicineId;
            Quantity = quantity;
            this.Price = price;
        }
        public MedicineInventory()
        {

        }

        [Key]
        public int MedicineId { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
