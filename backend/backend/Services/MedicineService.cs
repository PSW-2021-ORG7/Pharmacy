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

        public MedicineService(IMedicineRepository medicineRepository) => this.medicineRepository = medicineRepository;

        public List<Medicine> GetAll() => medicineRepository.GetAll();

        public void Save(Medicine medicine) => medicineRepository.Save(medicine);
    }
}
