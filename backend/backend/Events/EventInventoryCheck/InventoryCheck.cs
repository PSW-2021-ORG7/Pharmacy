using backend.Events.LogEvent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Events.EventInventoryCheck
{
        [Table(nameof(InventoryCheck), Schema = "Events")]
        public class InventoryCheck : Event
        {
        public string Name { get; set; }
        public int DosageInMg { get; set; }
    
    }

}
