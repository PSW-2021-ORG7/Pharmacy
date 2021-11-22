using backend.DTO;
using backend.Model;
using System;
using System.Collections.Generic;

namespace backend.Repositories.Interfaces
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        public bool MedicineExists(MedicineQuantityCheck DTO);
        public Medicine GetByName(string name);
        public Medicine GetByID(int id);
        public List<Medicine> MedicineSearchResults(MedicineSearchParams searchParams);
        public List<Medicine> MedicineFilterDosageResults(int from, int to);
        public Medicine GetByNameAndDose(string name, int dose);
        public String RequestSpecification(Medicine medicine);
    }
}
