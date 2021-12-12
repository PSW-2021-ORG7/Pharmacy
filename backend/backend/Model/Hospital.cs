using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model
{
    [Table("Hospital")]
    public class Hospital
    {
        [Key]
        public Guid ID { get; private set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid ApiKey { get; private set; }

        public string SiteLink { get; set; }
    }
}
