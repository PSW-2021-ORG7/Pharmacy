using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class MedicineQuantityCheck
    {
        public Guid MedicineId;
        public string Name;
        public int DosageInMg;
        public int Quantity;
    }
}
