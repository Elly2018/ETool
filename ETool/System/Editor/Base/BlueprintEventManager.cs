using System.Collections.Generic;

namespace ETool.ANode
{
    public class BlueprintEventManager
    {
        private List<ACustomEventCall> events = new List<ACustomEventCall>();

        public void AddEvent(ACustomEventCall target)
        {
            events.Add(target);
        }

        public void RemoveEvent()
        {
            events.RemoveAt(events.Count - 1);
        }

        public void SendReturn(object returnObj)
        {
            events[events.Count - 1].SetReturn(returnObj);
        }

        public void StartReturn(BlueprintInput data)
        {
            ACustomEventCall buffer = events[events.Count - 1];
            RemoveEvent();
            buffer.ActiveNextEvent(0, data);
        }
    }
}
