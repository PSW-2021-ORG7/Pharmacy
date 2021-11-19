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
            {
                Medicine foundMedicine = GetByNameAndDose(DTO.Name, DTO.DosageInMg);
                if (medicineInventoryRepository.CheckMedicineQuantity(new MedicineInventory(foundMedicine.MedicineId, DTO.Quantity)))
                    return true;
            }
               
            return false;
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

        public Medicine GetByNameAndDose(string name, int dose)
        {
            return medicineRepository.GetByNameAndDose(name,dose);
        }

        public bool DeleteMedicine(String id)
        {
            Medicine medicineToDelete = medicineRepository.GetById(id);
            if (medicineToDelete == null) return false;
            
            medicineRepository.Delete(medicineToDelete);
            return true;
        }
    }
}
