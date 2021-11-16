using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model
{
    public class MedicineInventory
    {
        public MedicineInventory(Guid medicineId)
        {
            MedicineId = medicineId;
            Quantity = 0;
        }

        [Key]
        public Guid MedicineId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
