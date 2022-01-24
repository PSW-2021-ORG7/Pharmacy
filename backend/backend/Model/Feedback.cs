using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model
{
    public class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdFeedback { get; set; }
        [Required]
        public int IdHospital { get; set; }
        [Required]
        public String ContentFeedback { get; set; }
    }
}
