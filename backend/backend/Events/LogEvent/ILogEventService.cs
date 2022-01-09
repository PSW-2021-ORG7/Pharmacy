using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Events.LogEvent
{
    public interface ILogEventService<in T> where T : EventParams
    {
        void LogEvent(T eventParams);
    }
}
