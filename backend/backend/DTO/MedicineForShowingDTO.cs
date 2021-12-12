using backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTO
{
    public class MedicineForShowingDTO
    {
        public Medicine Medicine { get; set; }
        public List<Medicine> MedicinesToCombineWith { get; set; }
    }
}
