using backend.DAL;
using backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories.Interfaces
{
    public interface IGenericRepository<T> 
    {
        List<T> GetAll();
        bool Save(T entity);
        void Delete(T entity);
        bool Update(T entity);
    }
}
