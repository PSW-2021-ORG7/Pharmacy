using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
