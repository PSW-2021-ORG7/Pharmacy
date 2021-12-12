using backend.DAL;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{ 
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DrugStoreContext dB;

        public OrdersRepository(DrugStoreContext dataContext) => dB = dataContext;

        public Order GetById(int id)
        {
            return dB.Order.SingleOrDefault(o => o.Order_Id == id);
        }

        public void Delete(Order entity)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            return dB.Order.ToList();
        }

        public bool Save(Order entity)
        {
            dB.Order.Add(entity);
            dB.SaveChanges();
            return true;
        }

        public bool Update(Order entity)
        {
            var result = dB.Order.SingleOrDefault(h => h.Order_Id == entity.Order_Id);
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
