using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class MedicineInventoryService
    {
        IMedicineInventoryRepository medicineInventoryRepository;
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
    }
}
