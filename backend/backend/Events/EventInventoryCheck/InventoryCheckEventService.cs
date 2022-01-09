using backend.Events.LogEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Events.EventInventoryCheck
{
    public class InventoryCheckEventService : ILogEventService<InventoryCheckEventParams>
    {    
            private readonly IInventoryCheckRepository _InventoryCheckRepository;

            public InventoryCheckEventService(IInventoryCheckRepository inventoryCheckRepository)
            {
                _InventoryCheckRepository = inventoryCheckRepository;
            }


            public void LogEvent(InventoryCheckEventParams eventParams)
            {
                var InventoryCheckEvent = new InventoryCheck
                {
                    TimeStamp = DateTime.Now,
                    Name = eventParams.Name,
                    DosageInMg = eventParams.DosageInMg
                };

                _InventoryCheckRepository.LogEvent(InventoryCheckEvent);
            }

            public List<InventoryCheck> GetAll()
            {
                return _InventoryCheckRepository.GetAll();
            }
        }
    
}
