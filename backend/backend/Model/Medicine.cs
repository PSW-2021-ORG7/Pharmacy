using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model
{
    public class Medicine
    {
        public Guid medicine_id;
        public string name;

        public Medicine(string name)
        {
            medicine_id = new Guid();
            this.name = name;
        }
    }
}
