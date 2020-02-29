using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    public class BlueprintEventManager
    {
        private List<Tuple<ACustomEventCall, ACustomEvent>> events = new List<Tuple<ACustomEventCall, ACustomEvent>>();

        public void AddEvent(ACustomEventCall target)
        {
            events.Add(new Tuple<ACustomEventCall, ACustomEvent>(target, null));
        }

        public void ApplyEvent(ACustomEvent target)
        {
            events[events.Count - 1] = new Tuple<ACustomEventCall, ACustomEvent>(events[events.Count - 1].Item1, target);
        }

        public void RemoveEvent()
        {
            events.RemoveAt(events.Count - 1);
        }

        public void SendReturn(object returnObj)
        {
            events[events.Count - 1].Item1.SetReturn(returnObj);
        }

        public void StartReturn(BlueprintInput data)
        {
            if(events.Count > 0)
            {
                ACustomEventCall buffer = events[events.Count - 1].Item1;
                RemoveEvent();
                buffer.ActiveNextEvent(0, data);
            }
        }

        public void NoneReturn(ACustomEvent target, BlueprintInput data)
        {
            if (events[events.Count - 1].Item2 == target)
            {
                StartReturn(data);
            }
        }
    }
}
