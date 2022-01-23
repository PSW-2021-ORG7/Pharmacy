using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Integration_API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiKeyAuth]
    public class FeedbackController : Controller
    {
        private FeedbackService feedbackService;

        public FeedbackController(IFeedbackRepository feedbackRepository, IConfiguration configuration)
        {
            feedbackService = new FeedbackService(feedbackRepository);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(feedbackService.GetAll());
        }

        [HttpPost]
        public IActionResult CreateFeedback([FromBody] Feedback feedback)
        {
            if (feedbackService.Save(feedback)) return Ok(true);
            else return BadRequest();
        }
    }
}
