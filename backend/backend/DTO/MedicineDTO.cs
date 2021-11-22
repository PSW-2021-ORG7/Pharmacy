using backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class MedicineDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DosageInMilligrams { get; set; }
        public string Manufacturer { get; set; }
        public List<string> SideEffects { get; set; }
        public List<string> PossibleReactions { get; set; }
        public string WayOfConsumption { get; set; }
        public string PotentialDangers { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<int> MedicinesToCombineWith { get; set; }
    }
}
