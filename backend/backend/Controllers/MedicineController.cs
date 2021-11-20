using AutoMapper;
using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Integration_API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiKeyAuth]
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

        [HttpGet("test")]
        public IActionResult GetTest()
        {
            return Ok("It works!");
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

        [HttpGet("{name}/{dose}")]
        public ActionResult<Medicine> GetMedicineByNameAndDose(string name, int dose)
        {
            Medicine medicine = medicineService.GetByNameAndDose(name, dose);

            if (medicine == null) return NotFound("This medicine doesn't exist.");
            return medicine;
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePharmacy(String id)
        {
            if(medicineService.DeleteMedicine(id)) return Ok("Successfully deleted");
            return NotFound("This medicine doesn't exist.");
        }


        // INVENTORY

        [HttpPost]
        [Route("/inventory/check")]
        public IActionResult CheckIfAvailable([FromBody] MedicineQuantityCheck DTO)
        {
            if (medicineService.CheckMedicineQuantity(DTO))
                return Ok(true);

            return Ok(false);
        }

        [HttpGet]
        [Route("/inventory")]
        public IActionResult GetInventory()
        {
            return Ok(medicineInventoryService.GetAll());
        }

        [HttpPut]
        [Route("/inventory/{id}")]
        public IActionResult UpdateInventory([FromBody] MedicineInventory medicineInventory)
        {
            return Ok(medicineInventoryService.Update(medicineInventory));
        }

    }
}
