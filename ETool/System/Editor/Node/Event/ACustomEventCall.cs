using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    public class ACustomEventCall : NodeBase
    {
        private object returnObj;
        private BlueprintCustomEvent MyTarget = null;
        private EBlueprint ETarget = null;

        public ACustomEventCall(Vector2 position, float width, float height) : base(position, width, height)
        {
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

            if (MyTarget.returnType != FieldType.Event)
                data.eventManager.AddEvent(this);

            if (isInherit)
            {
                /* This is outside node */
                ActiveInheritCustomEvent(data, _arg.ToArray(), targetEventOrVar.Split('.')[0]);
            }
            else
            {
                /* This is private event */
                ActiveCustomEvent(data, targetEventOrVar, _arg.ToArray());
            }
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            /* 
             * Search self blueprint for method first 
            */
            if (!isInherit)
            {
                /* Private */
                foreach (var i in data.blueprintCustomEvents)
                {
                    if (i.eventName == targetEventOrVar.Split('.')[1])
                    {
                        MyTarget = i;
                    }
                }
            }
            else
            {
                /* Inherit */
                if (data.inherit && data.inherit.GetInheritEvent() != null)
                {
                    foreach (var i in data.inherit.GetInheritEvent())
                    {
                        if (i.Item1.eventName == targetEventOrVar.Split('.')[1])
                        {
                            MyTarget = i.Item1;
                        }
                    }
                }
            }

            /* Arugment update */
            if (MyTarget == null) return;
            UpdateField();
        }

        public void SetReturn(object targetObj)
        {
            returnObj = targetObj;
        }

        public void SetCustomEvent(EBlueprint ebp, BlueprintCustomEvent bce)
        {
            MyTarget = bce;
            ETarget = ebp;
            targetEventOrVar = ebp.name + "." + bce.eventName;

            UpdateField();
        }

        public void UpdateField()
        {
            bool change = true;

            unlocalTitle = MyTarget.eventName;

            // Value amount
            while (change)
            {
                change = false;

                if(MyTarget.returnType == FieldType.Event)
                {
                    // No return
                    if (fields.Count > MyTarget.arugments.Count + 1)
                    {
                        change = true;
                        ACustomEvent.RemoveVariableField(this, true);
                    }
                    else if (fields.Count < MyTarget.arugments.Count + 1)
                    {
                        change = true;
                        ACustomEvent.AddVariableField(MyTarget.arugments[fields.Count - 1], this, true);
                    }
                }
                else
                {
                    // Have return
                    if (fields.Count > MyTarget.arugments.Count + 2)
                    {
                        change = true;
                        ACustomEvent.RemoveVariableField(this, true);
                    }
                    else if (fields.Count < MyTarget.arugments.Count + 2)
                    {
                        if(fields.Count == MyTarget.arugments.Count + 1)
                        {
                            change = true;
                            fields.Add(new Field(MyTarget.returnType, "Return", ConnectionType.DataOutput, true, this, MyTarget.returnContainer));
                        }
                        else
                        {
                            change = true;
                            ACustomEvent.AddVariableField(MyTarget.arugments[fields.Count - 1], this, true);
                        }
                    }
                }
            }

            /* Type check */
            if (MyTarget.returnType == FieldType.Event)
            {
                for (int i = 1; i < fields.Count; i++)
                {
                    if (!ACustomEvent.CheckArugmentMatch(MyTarget.arugments[i - 1], fields[i]))
                        ACustomEvent.ChangeVariableField(MyTarget.arugments[i - 1], this, i, true);
                }
            }
            else
            {
                for (int i = 1; i < fields.Count - 1; i++)
                {
                    if (!ACustomEvent.CheckArugmentMatch(MyTarget.arugments[i - 1], fields[i]))
                        ACustomEvent.ChangeVariableField(MyTarget.arugments[i - 1], this, i, true);
                }
                if(fields[fields.Count - 1].fieldType != MyTarget.returnType || fields[fields.Count - 1].fieldContainer != MyTarget.returnContainer)
                {
                    fields[fields.Count - 1] = new Field(MyTarget.returnType, "Return", ConnectionType.DataOutput, true, this, MyTarget.returnContainer);
                }
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

        [NodePropertyGet2(1, 99)]
        public object GetReturn(BlueprintInput data, int index)
        {
            return returnObj;
        }
    }
}
