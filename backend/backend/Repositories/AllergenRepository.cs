using backend.DAL;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class AllergenRepository : IAllergenRepository
    {
        private readonly DrugStoreContext _dataContext;

        public AllergenRepository(DrugStoreContext dataContext) => _dataContext = dataContext;

        public List<Allergen> GetAll() => _dataContext.Allergen.ToList();

        public bool Save(Allergen entity)
        {
            _dataContext.Allergen.Add(entity);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(Allergen entity)
        {
            return true;
        }

        public void Delete(Allergen entity)
        {
            throw new NotImplementedException();
        }

        public Allergen GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
