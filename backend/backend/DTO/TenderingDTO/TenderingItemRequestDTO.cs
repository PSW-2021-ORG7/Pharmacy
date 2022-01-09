using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO.TenderingDTO
{
    public class TenderingItemRequestDTO
    {
        public String MedicineName { get; set; }
        public int DosageInMilligrams { get; set; }
        public int RequiredQuantity { get; set; }

        public TenderingItemRequestDTO() { }
    }
}
