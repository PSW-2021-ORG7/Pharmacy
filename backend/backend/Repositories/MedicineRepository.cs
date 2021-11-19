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
            if (_dataContext.Medicine.Any(m => m.Name.Equals(DTO.Name)  && m.DosageInMilligrams.Equals(DTO.DosageInMg))) return true;
            return false;
        }

        public void Delete(Medicine medicine)
        {
            _dataContext.Medicine.Remove(medicine);
            _dataContext.SaveChanges();
        }

        public List<Medicine> GetAll() { 
            return _dataContext.Medicine.ToList();
         }

        public Medicine GetById(string id)
        {
            return _dataContext.Medicine.SingleOrDefault(m => m.MedicineId.ToString() == id);
        }

        public Medicine GetByNameAndDose(string name, int dose)
        {
            return _dataContext.Medicine.SingleOrDefault(m => m.Name == name && m.DosageInMilligrams == dose);
        }

        public bool Save(Medicine medicine)
        {
            if (_dataContext.Medicine.Any(m => m.Name == medicine.Name && m.DosageInMilligrams == medicine.DosageInMilligrams)) return false;

            _dataContext.Medicine.Add(medicine);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(Medicine medicine)
        {
            bool success = false;
            var result = _dataContext.Medicine.SingleOrDefault(m => m.MedicineId == medicine.MedicineId);
            if (result != null)
            {
                _dataContext.Update(medicine);
                _dataContext.SaveChanges();
                success = true;
            }
            return success;

        }

        public Medicine GetByID(Guid id)
        {
            return _dataContext.Medicine.SingleOrDefault(m => m.MedicineId.Equals(id));
        }

        public List<Medicine> MedicineSearchResults(MedicineSearchParams searchParams)
        {
            List<Medicine> searchResults = new List<Medicine>();
            searchResults = _dataContext.Medicine.Where(m => m.Name.ToLower().Contains(searchParams.Name.ToLower()) 
                                                          && m.Description.ToLower().Contains(searchParams.Description.ToLower())
                                                          && m.Manufacturer.ToLower().Contains(searchParams.Manufacturer.ToLower())).ToList();

            return searchResults;
        }

        public List<Medicine> MedicineFilterDosageResults(int from, int to)
        {
            List<Medicine> filterResults = new List<Medicine>();
            if(to < 600)
                filterResults = _dataContext.Medicine.Where(m => m.DosageInMilligrams >= from && m.DosageInMilligrams <= to).ToList();
            else
                filterResults = _dataContext.Medicine.Where(m => m.DosageInMilligrams >= to).ToList();
            return filterResults;
        }

        public bool DeleteMedicine(String id)
        {
            var medicine = _dataContext.Medicine.Find(id);
            if (medicine == null)
            {
                return false;
            }

            Delete(medicine);
            return true;
        }
    }
}
