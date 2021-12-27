using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO.TenderingDTO
{
    public class TenderingRequestDTO
    {
        public List<TenderingItemRequestDTO> requestedItems { get; set; }
        public TenderingRequestDTO() 
        {
            this.requestedItems = new List<TenderingItemRequestDTO>();
        }
    }
}
