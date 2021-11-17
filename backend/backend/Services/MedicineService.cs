﻿using backend.Model;
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
    }
}
