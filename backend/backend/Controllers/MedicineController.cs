using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineController : Controller
    {

        private readonly IConfiguration _configuration;
        private MedicineService medicineService;
        private AllergenService allergenService;

        public MedicineController(IConfiguration configuration,IMedicineRepository medicineRepository
            ,IAllergenRepository allergenRepository)
        {
            allergenService = new AllergenService(allergenRepository);
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
            medicineService.Save(medicine);
            return Ok("Succesfully added medicine");

        }

        
    }
}
