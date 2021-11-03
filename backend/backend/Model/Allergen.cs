using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model
{
    [Table("Allergen")]
    public class Allergen
    {
        [Key]
        public Guid AllergenId { get;private set; }

        public string Other { get; set; }
        public List<string> MedicineNames { get; set; }
        public List<string> IngredientNames { get; set; }
      
        public Allergen()
        {
            MedicineNames = new List<string>();
            IngredientNames = new List<string>();
            AllergenId = new Guid();
        }

    }
}
