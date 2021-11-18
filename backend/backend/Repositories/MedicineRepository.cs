using backend.DAL;
using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly DrugStoreContext _dataContext;

        public MedicineRepository(DrugStoreContext dataContext) => _dataContext = dataContext;

        public bool MedicineExists(MedicineQuantityCheck DTO)
        {
            if (_dataContext.Medicine.Any(m => m.MedicineId.Equals(DTO.MedicineId)  && m.DosageInMilligrams.Equals(DTO.DosageInMg))) return true;
            return false;
        }

        public void Delete(Medicine medicine)
        {
            throw new NotImplementedException();
        }

        public List<Medicine> GetAll() { 
            return _dataContext.Medicine.ToList();
         }

        public Medicine GetById(string id)
        {
            throw new NotImplementedException();
        }

       public Medicine getByName(string name)
        {
            return _dataContext.Medicine.SingleOrDefault(m => m.Name == name);
        }

        public bool Save(Medicine medicine)
        {
            if (_dataContext.Medicine.Any(m => m.Name == medicine.Name)) return false;

            _dataContext.Medicine.Add(medicine);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(Medicine medicine)
        {
            return true;
        }
    }
}
