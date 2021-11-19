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
        [JsonConstructor]
        public MedicineInventory(Guid medicineId)
        {
            MedicineId = medicineId;
            Quantity = 0;
        }

        public MedicineInventory(Guid medicineId,int quantity)
        {
            MedicineId = medicineId;
            Quantity = quantity;
        }

        [Key]
        public Guid MedicineId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
