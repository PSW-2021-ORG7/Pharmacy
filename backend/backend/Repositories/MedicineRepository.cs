using backend.DAL;
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

        public void Delete(Medicine entity)
        {
            throw new NotImplementedException();
        }

        public List<Medicine> GetAll() { 
            return _dataContext.Medicine.ToList();
         }

        public bool Save(Medicine entity)
        {
            _dataContext.Medicine.Add(entity);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(Medicine entity)
        {
            return true;
        }
    }
}
