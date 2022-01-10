using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class MedicineQuantityCheck
    {
        public string Name { get; set; }
        public int DosageInMg { get; set; }
        public int Quantity
        {
            get; set;
        }

        public MedicineQuantityCheck() { }
    }
}
