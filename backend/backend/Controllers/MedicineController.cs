using AutoMapper;
using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
        private MedicineCombinationService medicineCombinationService;

        public MedicineController(IConfiguration configuration, IMedicineRepository medicineRepository, IMedicineInventoryRepository medicineInventoryRepository,
            IMapper mapper, IMedicineCombinationRepository medicineCombinationRepository)
        {
            medicineService = new MedicineService(medicineRepository, medicineInventoryRepository);
            medicineInventoryService = new MedicineInventoryService(medicineInventoryRepository);
            medicineCombinationService = new MedicineCombinationService(medicineCombinationRepository);
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
                medicineInventoryService.Save(new MedicineInventory(medicine.Id));
                foreach (int m in medicineDTO.MedicinesToCombineWith)
                {
                    medicineCombinationService.Save(medicine.Id, m);
                }
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
        public IActionResult GetMedicineByID(int id)
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

        [HttpPut("update-inventory")]
        public IActionResult UpdateInventory([FromBody] MedicineInventory medicineInventory)
        {
            medicineInventoryService.Update(medicineInventory);
            return Ok("Succesfully updated inventory");
        }

        [HttpPut("reduce-quantity")]
        public IActionResult ReduceQuantity([FromBody] MedicineInventory medicineInventory)
        {
            medicineInventoryService.ReduceMedicineQuantity(medicineInventory);
            return Ok();
        }
    }
}
