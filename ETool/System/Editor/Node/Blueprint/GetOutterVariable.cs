using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Variable/Get External Variable")]
    public class GetOutterVariable : NodeBase
    {
        private EBlueprint ETarget;

        public GetOutterVariable(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get External Variable";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Blueprint, "BP Sample", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Blueprint, "BP Instance", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Dropdown, "BP Varaible", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(object), 3)]
        public object GetObj(BlueprintInput data)
        {
            EBlueprint target = (EBlueprint)GetFieldOrLastInputField(1, data);
            return target.CallGetVariable(target, (int)fields[2].GetValue(FieldType.Int), fields[3].fieldType);
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
            ETarget = (EBlueprint)fields[0].GetValue(FieldType.Blueprint);
            if (ETarget == null) { Zero(); return; }

            List<BlueprintVariable> buffer = ETarget.GetAllPublicVariable();
            if (buffer.Count == 0) { Zero(); return; }

            fields[2].target_array = new GenericObject[buffer.Count];
            for (int i = 0; i < buffer.Count; i++)
            {
                fields[2].target_array[i] = new GenericObject();
                fields[2].target_array[i].genericBasicType.target_String = buffer[i].label;
            }

            int index = (int)fields[3].GetValue(FieldType.Int);
            if (fields[3].fieldType != buffer[index].type)
                fields[3] = new Field(buffer[index].type, "Value", ConnectionType.DataBoth, this, FieldContainer.Object);
        }

        private void Zero()
        {
            fields[2].target_array = new GenericObject[0];
            fields[2].target.genericBasicType.target_Int = 0;
        }
    }
}
