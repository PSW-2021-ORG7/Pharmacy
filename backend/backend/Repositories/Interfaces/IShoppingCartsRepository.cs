using backend.Model;
using System;
using backend.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories.Interfaces
{
    public interface IShoppingCartsRepository : IGenericRepository<ShoppingCart>
    {
        public ShoppingCart GetByUserID(Guid userID);
    }
}
