using AutoMapper;
using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private MedicineService medicineService;
        private MedicineInventoryService medicineInventoryService;

        public MedicineController(IConfiguration configuration, IMedicineRepository medicineRepository, IMedicineInventoryRepository medicineInventoryRepository,
            IMapper mapper)
        {
            medicineService = new MedicineService(medicineRepository, medicineInventoryRepository);
            medicineInventoryService = new MedicineInventoryService(medicineInventoryRepository);
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(medicineService.GetAll());
        }

        [HttpPost]
        public IActionResult CreateMedicine([FromBody] MedicineDTO medicineDTO)
        {
            Medicine medicine = _mapper.Map<Medicine>(medicineDTO);

            if (medicineService.Save(medicine)) {
                medicineInventoryService.Save(new MedicineInventory(medicine.MedicineId));
                return Ok("Succesfully added medicine");
            }

            return BadRequest("Medicine with that name already exists");
        }

        [HttpGet("{name}")]
        public ActionResult<Medicine> GetMedicineByName(string name)
        {
            Medicine medicine = medicineService.getByName(name);

            if (medicine == null) return NotFound("This medicine doesn't exist.");
            return medicine;
        }

        
    }
}
