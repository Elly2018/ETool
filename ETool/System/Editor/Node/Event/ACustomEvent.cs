using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [CanNotDelete]
    public class ACustomEvent : NodeBase
    {
        private object[] obj;
        private BlueprintCustomEvent MyTarget = null;

        public ACustomEvent(Vector2 position, float width, float height) : base(position, width, height)
        {
        }

        public void ReceivedObject(object[] o)
        {
            obj = o;
        }

        [NodePropertyGet2(1, 100)]
        public object OutputData(BlueprintInput data, int index)
        {
            if (obj[index - 1] == null)
                return GetFieldOrLastInputField(index, data);
            else
                return obj[index - 1];
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            ActiveNextEvent(0, data);
            data.eventManager.StartReturn(data); // return null
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            for (int i = 0; i < data.blueprintCustomEvents.Count; i++)
            {
                if (i + EBlueprint.DefaultPageCount == page)
                {
                    MyTarget = data.blueprintCustomEvents[i];
                    break;
                }
            }

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
                    ACustomEvent.RemoveVariableField(this, false);
                }
                if (fields.Count < MyTarget.arugments.Count + 1)
                {
                    change = true;
                    ACustomEvent.AddVariableField(MyTarget.arugments[fields.Count - 1], this, false);
                }
            }

            for (int i = 1; i < fields.Count; i++)
            {
                if (!ACustomEvent.CheckArugmentMatch(MyTarget.arugments[i - 1], fields[i]))
                    ACustomEvent.ChangeVariableField(MyTarget.arugments[i - 1], this, i, false);
            }
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventOutput, this, FieldContainer.Object));
        }

        public override StyleType GetNodeStyle()
        {
            return StyleType.Event_Node;
        }

        public override StyleType GetNodeSelectStyle()
        {
            return StyleType.Select_Event_Node;
        }

        public static bool CheckArugmentsMatch(List<BlueprintVariable> bv, NodeBase nb)
        {
            if (bv.Count + 1 != nb.fields.Count) return false;
            for(int i = 1; i < nb.fields.Count; i++)
            {
                if (nb.fields[i].fieldType != bv[i - 1].type)
                    return false;
                if (nb.fields[i].title != bv[i - 1].label)
                    return false;
            }
            return true;
        }

        public static bool CheckArugmentMatch(BlueprintVariable v, Field f)
        {
            return f.fieldType == v.type && f.title == v.label;
        }

        public static void AddVariableField(BlueprintVariable v, NodeBase nb, bool call)
        {
            if(call)
                nb.fields.Add(new Field(v.type, v.label, ConnectionType.DataInput, true, nb, v.fieldContainer));
            else
                nb.fields.Add(new Field(v.type, v.label, ConnectionType.DataOutput, nb, v.fieldContainer));
        }

        public static void RemoveVariableField(NodeBase nb, bool call)
        {
            EBlueprint.GetBlueprintByNode(nb).Connection_RemoveRelateConnectionInField(nb.fields[nb.fields.Count - 1]);
            nb.fields.RemoveAt(nb.fields.Count - 1);
        }

        public static void ChangeVariableField(BlueprintVariable v, NodeBase nb, int index, bool call)
        {
            if(v.type != nb.fields[index].fieldType)
                EBlueprint.GetBlueprintByNode(nb).Connection_RemoveRelateConnectionInField(nb.fields[index]);
            if (call)
                nb.fields[index] = new Field(v.type, v.label, ConnectionType.DataInput, true, nb, v.fieldContainer);
            else
                nb.fields[index] = new Field(v.type, v.label, ConnectionType.DataOutput, nb, v.fieldContainer);
        }
    }
}
