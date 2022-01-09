using backend.DAL;
using backend.Events.LogEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Events.EventInventoryCheck
{
    class InventoryCheckDatabase : EventDatabase<InventoryCheck>, IInventoryCheckRepository
    {
        public InventoryCheckDatabase(DrugStoreContext dbContext) : base(dbContext)
        {
        }

        public override void LogEvent(InventoryCheck @event)
        {
            DbContext.InventoryCheck.Add(@event);
            DbContext.SaveChanges();
        }

        public List<InventoryCheck> GetAll()
        {
            foreach (InventoryCheck building in DbContext.InventoryCheck.ToList())
                DbContext.Entry(building).Reload();
            return DbContext.InventoryCheck.ToList();
        }
    }
}
