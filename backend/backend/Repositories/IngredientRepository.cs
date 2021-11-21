using backend.DAL;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly DrugStoreContext _dataContext;

        public IngredientRepository(DrugStoreContext dataContext) => _dataContext = dataContext;

        public void Delete(Ingredient ingredient)
        {
            _dataContext.Ingredient.Remove(ingredient);
            _dataContext.SaveChanges();
        }

        public List<Ingredient> GetAll()
        {
            return _dataContext.Ingredient.ToList();
        }

        public bool Save(Ingredient ingredient)
        {
            if (_dataContext.Ingredient.Any(i => i.Name == ingredient.Name)) return false;

            _dataContext.Ingredient.Add(ingredient);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(Ingredient ingredient)
        {
            bool success = false;
            var result = _dataContext.Ingredient.SingleOrDefault(m => m.Id == ingredient.Id);
            if (result != null)
            {
                _dataContext.Update(ingredient);
                _dataContext.SaveChanges();
                success = true;
            }
            return success;
        }
    }
}
