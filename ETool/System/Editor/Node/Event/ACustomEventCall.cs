using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    public class ACustomEventCall : NodeBase
    {
        private object[] obj;
        private BlueprintCustomEvent MyTarget = null;

        public ACustomEventCall(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            List<object> _arg = new List<object>();
            for (int i = 1; i < fields.Count; i++)
            {
                if (CheckIfConnectionExist(i, data, true))
                    _arg.Add(GetFieldInputValue(i, data));
                else
                    _arg.Add(null);

            }

            if (targetPage < EBlueprint.DefaultPageCount)
            {
                /* This is outside node */
                ActiveInheritCustomEvent(data, _arg.ToArray());
            }
            else
            {
                /* This is private event */
                
                ActiveCustomEvent(data, targetPage, _arg.ToArray());
            }
            ActiveNextEvent(0, data);
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            if (targetPage < EBlueprint.DefaultPageCount)
            {
                /* Inherit */
                if(data.inherit != null)
                {
                    foreach (var i in data.inherit.GetInheritEvent())
                    {
                        if (i.eventName == title)
                        {
                            MyTarget = i;
                        }
                    }
                }
            }
            else
            {
                /* Private */
                foreach(var i in data.blueprintCustomEvents)
                {
                    if(i.eventName == title)
                    {
                        MyTarget = i;
                    }
                }
            }

            /* Arugment update */
            if (MyTarget == null) return;
            UpdateField();
        }

        public void SetCustomEvent(BlueprintCustomEvent bce)
        {
            MyTarget = bce;
            UpdateField();
        }

        private void UpdateField()
        {
            bool change = true;
            while (change)
            {
                change = false;
                if (fields.Count > MyTarget.arugments.Count + 1)
                {
                    change = true;
                    ACustomEvent.RemoveVariableField(this, true);
                }
                if (fields.Count < MyTarget.arugments.Count + 1)
                {
                    change = true;
                    ACustomEvent.AddVariableField(MyTarget.arugments[fields.Count - 1], this, true);
                }
            }

            for (int i = 1; i < fields.Count; i++)
            {
                if (!ACustomEvent.CheckArugmentMatch(MyTarget.arugments[i - 1], fields[i]))
                    ACustomEvent.ChangeVariableField(MyTarget.arugments[i - 1], this, i, true);
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
