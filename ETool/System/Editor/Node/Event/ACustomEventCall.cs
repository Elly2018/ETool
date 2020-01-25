using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    public class ACustomEventCall : NodeBase
    {
        private object[] obj;

        public ACustomEventCall(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            List<object> _arg = new List<object>();
            for(int i = 1; i < fields.Count; i++)
            {
                if(CheckIfConnectionExist(i, data, true))
                    _arg.Add(GetFieldInputValue(i, data));
                else
                    _arg.Add(null);

            }
            ActiveCustomEvent(data, targetPage, _arg.ToArray());
            ActiveNextEvent(0, data);
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            BlueprintCustomEvent target = null;
            for (int i = 0; i < data.blueprintCustomEvents.Count; i++)
            {
                if (i + 2 == targetPage)
                {
                    target = data.blueprintCustomEvents[i];
                    break;
                }
            }
            if (target == null) return;

            bool change = true;
            while (change)
            {
                change = false;
                if (fields.Count > target.arugments.Count + 1)
                {
                    change = true;
                    ACustomEvent.RemoveVariableField(this, true);
                }
                if (fields.Count < target.arugments.Count + 1)
                {
                    change = true;
                    ACustomEvent.AddVariableField(target.arugments[fields.Count - 1], this, true);
                }
            }

            for (int i = 1; i < fields.Count; i++)
            {
                if (!ACustomEvent.CheckArugmentMatch(target.arugments[i - 1], fields[i]))
                    ACustomEvent.ChangeVariableField(target.arugments[i - 1], this, i, true);
            }
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
        }

        public override StyleType GetNodeStyle()
        {
            return StyleType.Event_Node;
        }

        public override StyleType GetNodeSelectStyle()
        {
            return StyleType.Select_Event_Node;
        }
    }
}
