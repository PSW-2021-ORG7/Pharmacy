﻿using backend.Model;
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
    }
}
