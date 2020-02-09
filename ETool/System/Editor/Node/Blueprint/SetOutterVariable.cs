using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Variable/Set External Variable")]
    public class SetOutterVariable : NodeBase
    {
        private EBlueprint ETarget;
        private object ObjBuffer;

        public SetOutterVariable(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set External Variable";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            EBlueprint target = (EBlueprint)GetFieldOrLastInputField(2, data);
            target.CallSetVariable(target, (int)fields[3].GetValue(FieldType.Int), fields[4].fieldType, GetFieldOrLastInputField(4, data));
            ActiveNextEvent(0, data);
        }

        [NodePropertyGet(typeof(object), 4)]
        public object GetObj(BlueprintInput data)
        {
            return ObjBuffer;
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Blueprint, "BP Sample", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Blueprint, "BP Instance", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Dropdown, "BP Varaible", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Value", ConnectionType.None, this, FieldContainer.Object));
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

            List<BlueprintVariable> buffer = ETarget.GetAllPublicVariable();
            if (buffer.Count == 0) { Zero(); return; }

            fields[3].target_array = new GenericObject[buffer.Count];
            for (int i = 0; i < buffer.Count; i++)
            {
                fields[3].target_array[i] = new GenericObject();
                fields[3].target_array[i].genericBasicType.target_String = buffer[i].label;
            }

            int index = (int)fields[3].GetValue(FieldType.Int);
            if(fields[4].fieldType != buffer[index].type)
                fields[4] = new Field(buffer[index].type, "Value", ConnectionType.DataBoth, this, FieldContainer.Object);
        }

        private void Zero()
        {
            fields[3].target_array = new GenericObject[0];
            fields[3].target.genericBasicType.target_Int = 0;

            while (fields.Count != 4)
            {
                ACustomEvent.RemoveVariableField(this, true);
            }
        }
    }
}
