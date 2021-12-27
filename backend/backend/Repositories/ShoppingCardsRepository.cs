using backend.DAL;
using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public class ShoppingCardsRepository : IShoppingCardsRepository
    {
        private readonly DrugStoreContext dB;

        public ShoppingCardsRepository(DrugStoreContext dataContext) => dB = dataContext;

        public void Delete(ShoppingCart entity)
        {
            dB.ShoppingCart.Remove(entity);
        }

        public List<ShoppingCart> GetAll()
        {
            return dB.ShoppingCart.Include(m => m).ToList();
        }

        public bool Save(ShoppingCart entity)
        {
            if (dB.ShoppingCart.Any(m => m.User == entity.User))
            {
                Update(entity);
                return false;
            }
            
            dB.ShoppingCart.Add(entity);
            dB.SaveChanges();
            return true;
        }

        public bool Update(ShoppingCart entity)
        {
            var result = dB.ShoppingCart.SingleOrDefault(s => s.ShoppingCart_Id == entity.ShoppingCart_Id);
            if (result != null)
            {
                result = entity;
                dB.SaveChanges();
                return true;
            }
            return false;
        }

       
    }
}
