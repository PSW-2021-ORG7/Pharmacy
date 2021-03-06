using backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories.Interfaces
{
    public interface IOrdersRepository : IGenericRepository<Order>
    {
        Order GetById(int id);
        List<Order> LoadRelatedEntities();
        bool Reorder(Order entity);
    }
}
