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
    public class ShoppingCartsRepository : IShoppingCartsRepository
    {
        private readonly DrugStoreContext dB;

        public ShoppingCartsRepository(DrugStoreContext dataContext) => dB = dataContext;

        public void Delete(ShoppingCart entity)
        {
            dB.ShoppingCart.Remove(entity);
        }

        public List<ShoppingCart> GetAll()
        {
            return dB.ShoppingCart.Include(m => m.User).ToList();
        }

        public ShoppingCart GetByUserID(Guid userID)
        {
            //userID = getFirstUserId();
            var cart = dB.ShoppingCart.Include(c => c.User).Include(b => b.ShoppingCartItem).ThenInclude(sci => sci.Medicine).FirstOrDefault(s => s.User.UserId == userID);
            return cart;
        }


        private Guid getFirstUserId()
        {
            return GetAll()[0].User.UserId;
        }

        public bool Save(ShoppingCart entity)
        {
            if (dB.ShoppingCart.Any(m => m.User.UserId.Equals(entity.User.UserId)))
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

        public ShoppingCart GetByID(int id)
        {
            return dB.ShoppingCart.Include(c => c.User).Include(b => b.ShoppingCartItem).ThenInclude(sci => sci.Medicine).SingleOrDefault(s => s.ShoppingCart_Id == id);
        }
    }
}
