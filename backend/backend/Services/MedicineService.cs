using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class MedicineService
    {
        IMedicineRepository medicineRepository;
        IMedicineInventoryRepository medicineInventoryRepository;
        public MedicineService(IMedicineRepository medicineRepository, IMedicineInventoryRepository medicineInventoryRepository) { 
            this.medicineRepository = medicineRepository;
            this.medicineInventoryRepository = medicineInventoryRepository;
        }

        public List<Medicine> GetAll() => medicineRepository.GetAll();

        public bool Save(Medicine medicine) {
            if (medicineRepository.Save(medicine)) return true;
            return false;
        }

        public bool CheckMedicineQuantity(MedicineQuantityCheck DTO)
        {
            if (medicineRepository.MedicineExists(DTO))
               if(medicineInventoryRepository.CheckMedicineQuantity(new MedicineInventory(DTO.MedicineId,DTO.Quantity)))
                    return true;
            return false;
        }

        public Medicine GetByName(string name)
        {
            return medicineRepository.GetByName(name);
        }

        public Medicine GetByID(Guid id)
        {
            return medicineRepository.GetByID(id);
        }

        public List<Medicine> MedicineSearchResults(MedicineSearchParams searchParams)
        {
            return medicineRepository.MedicineSearchResults(searchParams);
        }

        public List<Medicine> MedicineFilterDosageResults(int option)
        {
            int from;
            int to = option;
            if (to > 100) from = option - 100;
            else from = 0;

            return medicineRepository.MedicineFilterDosageResults(from, to);
        }
    }
}
