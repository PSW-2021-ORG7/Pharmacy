using backend.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Events.LogEvent
{
    public abstract class EventDatabase<T> : IEventRepository<T> where T : Event
    {
        protected readonly DrugStoreContext DbContext;

        protected EventDatabase(DrugStoreContext dbContext)
        {
            DbContext = dbContext;
        }

        protected EventDatabase()
        {
        }

        public abstract void LogEvent(T @event);
    }
}
