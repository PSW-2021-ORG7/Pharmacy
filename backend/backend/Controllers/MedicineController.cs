using AutoMapper;
using backend.DTO;
using backend.Model;
using backend.Model.Enum;
using backend.Repositories.Interfaces;
using backend.Services;
using Grpc.Net.Client;
using Integration_API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiKeyAuth]
    public class MedicineController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private MedicineService _medicineService;
        private MedicineInventoryService _medicineInventoryService;
        private MedicineCombinationService _medicineCombinationService;

        public MedicineController(IConfiguration configuration, IMapper mapper, IMedicineRepository medicineRepository, 
            IMedicineInventoryRepository medicineInventoryRepository, IMedicineCombinationRepository medicineCombinationRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _medicineService = new MedicineService(medicineRepository, medicineInventoryRepository);
            _medicineInventoryService = new MedicineInventoryService(medicineInventoryRepository);
            _medicineCombinationService = new MedicineCombinationService(medicineCombinationRepository);
        }

        [HttpGet("test")]
        public IActionResult GetTest()
        {
           
            
            return Ok("It works");
        }
		
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_medicineService.GetAll());
        }

       
        [HttpPost]
        public IActionResult CreateMedicine([FromBody] NewMedicineDTO medicineDTO)
        {
            Medicine medicine = _mapper.Map<Medicine>(medicineDTO);

            if (_medicineService.Save(medicine)) {
                _medicineInventoryService.Save(new MedicineInventory(medicine.Id));
                foreach (int m in medicineDTO.MedicinesToCombineWith)
                {
                    _medicineCombinationService.Save(medicine.Id, m);
                }
                return Ok("Succesfully added medicine");
            }
            return BadRequest("Medicine with that name and dosage already exists");
        }

        [HttpGet("name/{name}")]
        public ActionResult<Medicine> GetMedicineByName(string name)
        {
            Medicine medicine = _medicineService.GetByName(name);
            if (medicine == null) return NotFound("This medicine doesn't exist.");

            MedicineForShowingDTO dto = FindMedicineCombination(medicine);
            return Ok(dto);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetMedicineByID(int id)
        {
            Medicine medicine = _medicineService.GetByID(id);
            if (medicine == null) return NotFound("This medicine doesn't exist.");

            MedicineForShowingDTO dto = FindMedicineCombination(medicine);

            return Ok(dto);
        }

        [HttpGet("search")]
        public IActionResult SearchMedicine([FromBody] MedicineSearchParams searchParams) 
        {
            return Ok(_medicineService.MedicineSearchResults(searchParams));
        }

        [HttpGet("filter/{option}")]
        public IActionResult FilterMedicineByDosage(MedicineDosageFilter option)
        {
            return Ok(_medicineService.MedicineFilterDosageResults(option));
        }

        private MedicineForShowingDTO FindMedicineCombination(Medicine medicine)
        {
            MedicineForShowingDTO dto = new MedicineForShowingDTO() { Medicine = medicine, MedicinesToCombineWith = new List<Medicine>() };
            List<MedicineCombination> medicinesToCombineWith = _medicineCombinationService.GetMedicinesCombination(medicine.Id);
            medicinesToCombineWith.ForEach(m => dto.MedicinesToCombineWith.Add(_medicineService.GetByID(m.SecondMedicineId)));

            return dto;
        }

        [HttpGet("find/{name}/{dose}")]
        public ActionResult<Medicine> GetMedicineByNameAndDose(string name, int dose)
        {
            Medicine medicine = _medicineService.GetByNameAndDose(name, dose);

            if (medicine == null) return NotFound("This medicine doesn't exist.");
            return medicine;
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePharmacy(int id)
        {
            if(_medicineService.DeleteMedicine(id)) return Ok("Successfully deleted");
            return NotFound("This medicine doesn't exist.");
        }

        [HttpGet("request/{name}/{dose}")]

        public IActionResult RequestSpecification(string name, int dose)
        {
            return Ok(JsonConvert.SerializeObject(_medicineService.RequestSpecification(name, dose)));
        }


        // INVENTORY

        [HttpPost]
        [Route("/inventory/check")]
        public IActionResult CheckIfAvailable([FromBody] MedicineQuantityCheck quantityCheck)
        {
            if (_medicineService.CheckMedicineQuantity(quantityCheck))
                return Ok(true);

            return BadRequest(false);
        }

        [HttpGet]
        [Route("/inventory")]
        public IActionResult GetInventory()
        {
            return Ok(_medicineInventoryService.GetAll());
        }

        [HttpPut]
        [Route("/inventory/{id}")]
        public IActionResult UpdateInventory([FromBody] MedicineInventory medicineInventory)
        {
            return Ok(_medicineInventoryService.UpdateMedicinePrice(medicineInventory));
        }

        [HttpPut]
        [Route("/inventory/change-price")]
        public IActionResult UpdateMedicinePrice([FromBody] MedicineInventory medicineInventory)
        {
            return Ok(_medicineInventoryService.Update(medicineInventory));
        }

        [HttpPut("/inventory/reduce-quantity")]
        public IActionResult ReduceQuantity([FromBody] MedicineInventory medicineInventory)
        {
            _medicineInventoryService.ReduceMedicineQuantity(medicineInventory);
            return Ok();
        }

        //Prescriptions

        [HttpGet("downloadPrescription/{fileName}")]
        public IActionResult DownloadPrescriptionSFTP(string fileName)
        {
            return Ok(_medicineService.DownloadPrescriptionSFTP(fileName));
        }

    }
}
