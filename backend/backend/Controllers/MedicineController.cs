using backend.DAL;
using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineController : Controller
    {

        private readonly IConfiguration _configuration;
        private MedicineService medicineService;

        public MedicineController(IConfiguration configuration,IMedicineRepository medicineRepository)
        {
            medicineService = new MedicineService(medicineRepository);
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(medicineService.GetAll());
        }
        [HttpPost]
         public IActionResult CreateMedicine()
        {
            var medicine = new Medicine("mezym");
            medicineService.Save(medicine);
            return Ok("Succesfully added medicine");

        }

        
    }
}
