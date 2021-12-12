using backend.DAL;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly DrugStoreContext dB;

        public HospitalRepository(DrugStoreContext dataContext) => dB = dataContext;

        public void Delete(Hospital entity)
        {
            throw new NotImplementedException();
        }

        public List<Hospital> GetAll()
        {
            return dB.Hospital.ToList();
        }

        public bool Save(Hospital entity)
        {
            if (dB.Hospital.Any(h => h.ApiKey == entity.ApiKey)) return false;

                dB.Hospital.Add(entity);
            dB.SaveChanges();
            return true;
        }

        public bool Update(Hospital entity)
        {
            var result = dB.Hospital.SingleOrDefault(h =>h.ID == entity.ID && h.ApiKey == entity.ApiKey);
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
