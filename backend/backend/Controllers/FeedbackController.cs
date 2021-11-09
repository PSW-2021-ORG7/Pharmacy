using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        private FeedbackService feedbackService;
        private readonly IConfiguration _configuration;

        public FeedbackController(IFeedbackRepository feedbackRepository, IConfiguration configuration)
        {
            this._configuration = configuration;
            feedbackService = new FeedbackService(feedbackRepository);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(feedbackService.GetAll());
        }

        // POST: api/feedback
        [HttpPost]
        public IActionResult CreateFeedback([FromBody] Feedback feedback)
        {
            Console.WriteLine("Hit the controller!");
            feedbackService.Save(feedback);
            return Ok("Succesfully added feedback!");

        }
    }
}
