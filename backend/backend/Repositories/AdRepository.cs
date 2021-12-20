using backend.DAL;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public class AdRepository : IAdRepository
    {
        private readonly DrugStoreContext _dataContext;

        public AdRepository(DrugStoreContext dataContext) => _dataContext = dataContext;

        public void Delete(Ad ad)
        {
            _dataContext.Ad.Remove(ad);
            _dataContext.SaveChanges();
        }

        public List<Ad> GetAll()
        {
            return _dataContext.Ad.ToList();
        }

        public bool Save(Ad ad)
        {
            _dataContext.Ad.Add(ad);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(Ad ad)
        {
            bool success = false;
            var result = _dataContext.Ad.SingleOrDefault(a => a.Id == ad.Id);
            if (result != null)
            {
                result = ad;
                _dataContext.Update(result);
                _dataContext.SaveChanges();
                success = true;
            }
            return success;
        }
    }
}
