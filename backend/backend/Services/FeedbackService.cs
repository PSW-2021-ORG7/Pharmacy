using backend.Model;
using backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class FeedbackService 
    {
        IFeedbackRepository feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository) => this.feedbackRepository = feedbackRepository;

        public List<Feedback> GetAll() => feedbackRepository.GetAll();

        public bool Save(Feedback feedback) => feedbackRepository.Save(feedback);
    }
}
