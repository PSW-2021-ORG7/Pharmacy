using backend.Events.LogEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Events.EventInventoryCheck
{
    public class InventoryCheckEventParams : EventParams
    {
        public string Name { get; set; }
        public int DosageInMg { get; set; }

        public InventoryCheckEventParams(string Name, int Dosage)
        {
            this.Name = Name;
            this.DosageInMg = Dosage;
        }

        public InventoryCheckEventParams(int dosageInMg, string name)
        {
            DosageInMg = dosageInMg;
            Name = name;
        }
    }
}
