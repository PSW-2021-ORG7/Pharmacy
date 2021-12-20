using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class AdDTO
    {
        public String Title { get; set; }

        public String Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
