using System;
using System.ComponentModel.DataAnnotations;


namespace backend.Model
{
    public class Feedback
    {
        [Key]
        public String IdFeedback { get; set; }
        [Required]
        public String IdHospital { get; set; }
        [Required]
        public String ContentFeedback { get; set; }
    }
}
