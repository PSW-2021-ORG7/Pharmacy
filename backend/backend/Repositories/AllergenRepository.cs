using backend.DAL;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public class AllergenRepository : IAllergenRepository
    {
        private readonly DrugStoreContext _dataContext;

        public AllergenRepository(DrugStoreContext dataContext) => _dataContext = dataContext;

        public List<Allergen> GetAll() => _dataContext.Allergen.ToList();

        public void Save(Allergen entity)
        {
            _dataContext.Allergen.Add(entity);
            _dataContext.SaveChanges();
        }

        public void Update(Allergen entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Allergen entity)
        {
            throw new NotImplementedException();
        }
    }
}
