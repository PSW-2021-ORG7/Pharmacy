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

            return BadRequest("Medicine with that name and dosage already exists");
        }

        [HttpGet("name/{name}")]
        public ActionResult<Medicine> GetMedicineByName(string name)
        {
            Medicine medicine = medicineService.GetByName(name);

            if (medicine == null) return NotFound("This medicine doesn't exist.");
            return medicine;
        }

        [HttpGet("id/{id}")]
        public IActionResult GetMedicineByID(Guid id)
        {
            Medicine medicine = medicineService.GetByID(id);

            if (medicine == null) return NotFound("This medicine doesn't exist.");
            return Ok(medicine);
        }

        [HttpGet("search")]
        public IActionResult SearchMedicine([FromBody] MedicineSearchParams searchParams) 
        {
            return Ok(medicineService.MedicineSearchResults(searchParams));
        }

        [HttpGet("filter/{dosage}")]
        public IActionResult FilterMedicineByDosage(int dosage)
        {
            return Ok(medicineService.MedicineFilterDosageResults(dosage));
        }
    }
}
