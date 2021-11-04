using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model
{
    [Table("Medicine")]
    public class Medicine
    {
        [Key]
        public Guid MedicineId { get; private set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int DosageInMilligrams { get; set; }

        public List<string> SideEffect { get; set; }

        [Required]
        public string WayOfConsumption { get; set; }

        public List<Allergen> Allergens { get; set; }

        public Medicine()
        {
            MedicineId = new Guid();
            Allergens = new List<Allergen>();
            SideEffect = new List<string>();
        }
    }
}
