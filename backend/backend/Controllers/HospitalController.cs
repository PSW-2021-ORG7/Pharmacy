using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HospitalController : Controller
    {
        private HospitalService hospitalService;
        private readonly IConfiguration _configuration;

        public HospitalController(IHospitalRepository hospitalRepository, IConfiguration configuration)
        {
            this._configuration = configuration;
            hospitalService = new HospitalService(hospitalRepository);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(hospitalService.GetAll());
        }

        [HttpPost]
        public IActionResult CreateHospital()
        {
            return Ok("Succesfully found hospital API");
        }
        [HttpPut]
        public IActionResult UpdateHospital(Hospital hospital)
        {
            return Ok("Succesfully found hospital API");
        }
    }
}
