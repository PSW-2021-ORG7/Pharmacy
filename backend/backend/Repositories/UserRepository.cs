using backend.DAL;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DrugStoreContext _dataContext;

        public UserRepository(DrugStoreContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Delete(User entity)
        {
            _dataContext.User.Remove(entity);
            _dataContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _dataContext.User.ToList();
        }

         
        public User GetById(string username)
        {
            return _dataContext.User.Find(username);
        }

        public bool Save(User entity)
        {
            if (_dataContext.User.Any(u => u.Username == entity.Username))
                return false;

            _dataContext.User.Add(entity);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
