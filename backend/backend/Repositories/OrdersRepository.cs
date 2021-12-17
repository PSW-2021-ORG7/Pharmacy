using backend.DAL;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public bool Reorder(Order entity)
        {
            List<OrderItem> alteredItems = new List<OrderItem>();
            foreach (OrderItem item in entity.OrderItems)
                alteredItems.Add(new OrderItem(item));
            entity.OrderItems = alteredItems;
            //entity.User = null;
            dB.Entry(entity.User).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Detached;
            dB.Order.Add(entity);
            dB.SaveChanges();
            return true;
        }

        public List<Order> LoadRelatedEntities()
        {
            List<Order> orders = GetAll();
            foreach (Order order in orders)
            {
                dB.Entry(order).Reference("User").Load();
                dB.Entry(order).Collection(o => o.OrderItems).Load();
                foreach(OrderItem orderItem in order.OrderItems)
                    dB.Entry(orderItem).Reference("Medicine").Load();
            }
            return orders;
        }

        public bool Update(Order entity)
        {
            var result = dB.Order.SingleOrDefault(h => h.Order_Id == entity.Order_Id);
            if (result != null)
            {
                dB.Entry(result).CurrentValues.SetValues(entity);
                dB.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
