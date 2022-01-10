using backend.Events.LogEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Events.EventInventoryCheck
{
    public interface IInventoryCheckRepository : IEventRepository<InventoryCheck>
    {
            List<InventoryCheck> GetAll();
       
    }
}
