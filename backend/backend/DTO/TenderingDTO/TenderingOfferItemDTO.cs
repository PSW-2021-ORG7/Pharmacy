using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO.TenderingDTO
{
    public class TenderingOfferItemDTO
    {
        public String MedicineName { get; set; }
        public String MedicineDosage { get; set; }
        public String AvailableQuantity { get; set; }
        public String MissingQuantity { get; set; }
        public String PriceForSingleEntity { get; set; }
        public String PriceForAllAvailableEntity { get; set; }
        public String PriceForAllRequiredEntity { get; set; }

        public TenderingOfferItemDTO() { }
    }
}
