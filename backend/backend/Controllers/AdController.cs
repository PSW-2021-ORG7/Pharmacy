using AutoMapper;
using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Integration_API.Filters;
using Microsoft.AspNetCore.Mvc;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdController : Controller
    {
        private readonly IConfiguration _configuration;
        private AdService adService;
        private readonly IMapper _mapper;

        public AdController(IAdRepository adRepository, IConfiguration configuration, IMapper mapper)
        {
            this._mapper = mapper;
            this._configuration = configuration;
            adService = new AdService(adRepository);
        }

        [HttpPost]
        public IActionResult CreateAd([FromBody] AdDTO dto)
        {
            Ad ad = _mapper.Map<Ad>(dto);

            if (adService.Save(ad))
            {
                return Ok("Succesfully added ad");
            }
            return BadRequest("Something went wrong");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(adService.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            adService.Delete(id);
            return Ok();
        }
    }
}
