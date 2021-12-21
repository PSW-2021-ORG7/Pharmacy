using backend.DTO.TenderingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class TenderingOfferDTO
    {
        public List<TenderingOfferItemDTO> tenderingOfferItems { get; set; }
        public String PriceForAllAvailable { get; set; }
        public String PriceForAllRequired { get; set; }
        public String TotalNumberMissingMedicine { get; set; }

        public TenderingOfferDTO() { }
    }
}
