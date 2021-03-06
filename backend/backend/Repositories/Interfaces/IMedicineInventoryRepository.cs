using backend.DTO.TenderingDTO;
using backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories.Interfaces
{
    public interface IMedicineInventoryRepository : IGenericRepository<MedicineInventory>
    {
        public bool CheckMedicineQuantity(MedicineInventory medicineInventory);
        public bool ReduceMedicineQuantity(MedicineInventory entity);
        public MedicineInventory FindRequestedMedicineInventory(TenderingItemRequestDTO tenderingItemRequestDTO);
    }
}
