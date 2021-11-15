using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Integration_API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiKeyAuth]
    public class MedicineController : Controller
    {

        private readonly IConfiguration _configuration;
        private MedicineService medicineService;
        private AllergenService allergenService;
        private MedicineInventoryService medicineInventoryService;

        public MedicineController(IConfiguration configuration,IMedicineRepository medicineRepository
            ,IAllergenRepository allergenRepository,IMedicineInventoryRepository medicineInventoryRepository)
        {
            allergenService = new AllergenService(allergenRepository);
             medicineService = new MedicineService(medicineRepository, medicineInventoryRepository);
            medicineInventoryService = new MedicineInventoryService(medicineInventoryRepository);
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
            var medicine = new Medicine();
            medicine.Name = "panadol";
            medicine.SideEffect.Add("mucnina");
            medicine.SideEffect.Add("glavobolja");
            medicine.DosageInMilligrams = 300;
            medicine.WayOfConsumption = "Posle jela";
            var allergen = new Allergen();
            allergen.IngredientNames.Add("sok");
            allergen.IngredientNames.Add("voda");
            medicine.Allergens.Add(allergen);
            allergenService.Save(allergen);
            if (medicineService.Save(medicine)) {
                medicineInventoryService.Save(new MedicineInventory(medicine.MedicineId));
                return Ok("Succesfully added medicine");
            }

            return BadRequest("Medicine with that name already exists");

        }

        
    }
}
