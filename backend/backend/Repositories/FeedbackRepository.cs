using backend.DAL;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DrugStoreContext _dataContext;
        public FeedbackRepository(DrugStoreContext dataContext) => _dataContext = dataContext;
        public void Delete(Feedback entity)
        {
            throw new NotImplementedException();
        }

        public List<Feedback> GetAll()
        {
            return _dataContext.Feedback.ToList();
        }

        public Feedback GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Feedback entity)
        {
            _dataContext.Feedback.Add(entity);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(Feedback entity)
        {
            throw new NotImplementedException();
        }
    }
}
