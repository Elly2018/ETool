using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Event/External Call")]
    [CanNotCopy]
    public class AOutterEventCall : NodeBase
    {
        private EBlueprint ETarget;
        private BlueprintCustomEvent MyTarget = null;

        public AOutterEventCall(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "External Event Call";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            List<object> _arg = new List<object>();
            for (int i = 4; i < fields.Count; i++)
            {
                if (CheckIfConnectionExist(i, data, true))
                    _arg.Add(GetFieldInputValue(i, data));
                else
                    _arg.Add(null);

            }
            Material c;
            EBlueprint target = (EBlueprint)GetFieldOrLastInputField(2, data);
            target.CallCustomEvent(target, fields[3].target_array[fields[3].target.genericBasicType.target_Int].genericBasicType.target_String, _arg.ToArray());
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Blueprint, "BP Sample", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Blueprint, "BP Instance", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Dropdown, "BP Function", ConnectionType.None, this, FieldContainer.Object));
        }

        public override void FieldUpdate()
        {
            UpdateContent();
        }

        public override void FinalFieldInitialize(BlueprintInput data)
        {
            UpdateContent();
        }

        private void UpdateContent()
        {
            ETarget = (EBlueprint)fields[1].GetValue(FieldType.Blueprint);
            if (ETarget == null) { Zero(); return; } 

            List<BlueprintCustomEvent> buffer = ETarget.GetAllPublicEvent();
            if (buffer.Count == 0) { Zero(); return; }

            fields[3].target_array = new GenericObject[buffer.Count];
            for (int i = 0; i < buffer.Count; i++)
            {
                fields[3].target_array[i] = new GenericObject();
                fields[3].target_array[i].genericBasicType.target_String = buffer[i].eventName;
            }

            int index = (int)fields[3].GetValue(FieldType.Int);
            if(index > -1 && index <= buffer.Count)
            {
                MyTarget = buffer[index];
                UpdateField();
            }
        }

        private void Zero()
        {
            fields[3].target_array = new GenericObject[0];
            fields[3].target.genericBasicType.target_Int = 0;

            while(fields.Count != 4)
            {
                ACustomEvent.RemoveVariableField(this, true);
            }
        }

        private void UpdateField()
        {
            bool change = true;
            while (change)
            {
                change = false;
                if (fields.Count > MyTarget.arugments.Count + 4)
                {
                    change = true;
                    ACustomEvent.RemoveVariableField(this, true);
                }
                if (fields.Count < MyTarget.arugments.Count + 4)
                {
                    change = true;
                    ACustomEvent.AddVariableField(MyTarget.arugments[fields.Count - 4], this, true);
                }
            }

            for (int i = 4; i < fields.Count; i++)
            {
                if (!ACustomEvent.CheckArugmentMatch(MyTarget.arugments[i - 4], fields[i]))
                    ACustomEvent.ChangeVariableField(MyTarget.arugments[i - 4], this, i, true);
            }
        }
    }
}
