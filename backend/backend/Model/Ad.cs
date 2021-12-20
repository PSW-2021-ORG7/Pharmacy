using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace backend.Model
{
    [Table("Ad")]
    public class Ad
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; private set; }

        [Required]
        public String Title { get; set; }

        [Required]
        public String Content { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool ValidDates()
        {
            if(CreationDate > EndDate) throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
            return true;
        }
    }
}
