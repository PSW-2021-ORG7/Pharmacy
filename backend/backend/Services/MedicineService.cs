using backend.DTO;
using backend.Model;
using backend.Model.Enum;
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

        public MedicineService(IMedicineRepository medicineRepository, IMedicineInventoryRepository medicineInventoryRepository)
        {
            this.medicineRepository = medicineRepository;
            this.medicineInventoryRepository = medicineInventoryRepository;
        }

        public List<Medicine> GetAll() => medicineRepository.GetAll();

        public bool Save(Medicine medicine)
        {
            if (medicineRepository.Save(medicine)) return true;
            return false;
        }

        public bool CheckMedicineQuantity(MedicineQuantityCheck Quantitycheck)
        {
            if (medicineRepository.MedicineExists(Quantitycheck))
            {
                Medicine foundMedicine = GetByNameAndDose(Quantitycheck.Name, Quantitycheck.DosageInMg);
                if (medicineInventoryRepository.CheckMedicineQuantity(new MedicineInventory(foundMedicine.Id, Quantitycheck.Quantity)))
                    return true;
            }

            return false;
        }

        public Medicine GetByName(string name)
        {
            return medicineRepository.GetByName(name);
        }

        public Medicine GetByID(int id)
        {
            return medicineRepository.GetByID(id);
        }

        public List<Medicine> MedicineSearchResults(MedicineSearchParams searchParams)
        {
            return medicineRepository.GetAll().Where(m => m.Name.ToLower().Contains(searchParams.Name.ToLower())
                                                          && m.Description.ToLower().Contains(searchParams.Description.ToLower())
                                                          && m.Manufacturer.ToLower().Contains(searchParams.Manufacturer.ToLower())
                                                          && m.Ingredients.Any(i => i.Name.ToLower().Contains(searchParams.Ingredient.ToLower())))
                                                 .ToList();
        }

        public List<Medicine> MedicineFilterDosageResults(MedicineDosageFilter option)
        {
            int from;
            int to;
            switch ((int)option)
            {
                case 0: from = 0; to = 200; break;
                case 1: from = 200; to = 400; break;
                case 2: from = 400; to = 600; break;
                default: from = 600; to = int.MaxValue; break;
            }

            return medicineRepository.GetAll().Where(m => m.DosageInMilligrams >= from && m.DosageInMilligrams <= to).ToList();
        }

        public Medicine GetByNameAndDose(string name, int dose)
        {
            return medicineRepository.GetByNameAndDose(name, dose);
        }

        public bool DeleteMedicine(int id)
        {
            Medicine medicineToDelete = medicineRepository.GetByID(id);
            if (medicineToDelete == null) return false;

            medicineRepository.Delete(medicineToDelete);
            return true;
        }

        public String RequestSpecification(string name, int dose)
        {
            Medicine medicine = GetByNameAndDose(name, dose);
            return medicineRepository.RequestSpecification(medicine);

        }

        public bool DownloadPrescriptionSFTP(String fileName)
        {
            try
            {
                return medicineRepository.DownloadPrescriptionSFTP(fileName);
            }
            catch (Exception e)
            {
                throw (e);
            }


        }
    }
}
