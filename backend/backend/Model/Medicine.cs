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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
<<<<<<< HEAD
        public int Id { get; private set; }
=======

        public Guid MedicineId { get; set; } 
>>>>>>> 7614e23 (refactor: Changed function names and parameteres)

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int DosageInMilligrams { get; set; }

        public string Manufacturer { get; set; }

        [Required]
        public List<string> SideEffects { get; set; }

        [Required]
        public List<string> PossibleReactions { get; set; }

        [Required]
        public string WayOfConsumption { get; set; }

        public string PotentialDangers { get; set; }

<<<<<<< HEAD
        public List<Ingredient> Ingredients { get; set; }

        public Medicine() {}
=======
        public Medicine()
        {
            MedicineId = new Guid();
            
        }
>>>>>>> 7614e23 (refactor: Changed function names and parameteres)
    }
}
