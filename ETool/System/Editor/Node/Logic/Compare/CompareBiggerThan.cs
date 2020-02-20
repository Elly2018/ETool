using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Logic/Compare/Bigger")]
    [Logic_Menu("Compare")]
    public class CompareBiggerThan : NodeBase
    {
        public CompareBiggerThan(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Number A > Number B";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Number, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Number A", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Number B", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 3)]
        public bool GetResult(BlueprintInput data)
        {
            FieldType ft = (FieldType)fields[0].GetValue(FieldType.Number);
            switch (ft)
            {
                case FieldType.Int:
                    return (int)GetFieldOrLastInputField(1, data) > (int)GetFieldOrLastInputField(2, data);
                case FieldType.Long:
                    return (long)GetFieldOrLastInputField(1, data) > (long)GetFieldOrLastInputField(2, data);
                case FieldType.Float:
                    return (float)GetFieldOrLastInputField(1, data) > (float)GetFieldOrLastInputField(2, data);
                case FieldType.Double:
                    return (double)GetFieldOrLastInputField(1, data) > (double)GetFieldOrLastInputField(2, data);
            }
            return (int)GetFieldOrLastInputField(0, data) > (int)GetFieldOrLastInputField(1, data);
        }

        public override void FieldUpdate()
        {
            TypeUpdate();
        }

        public override void DynamicFieldInitialize(BlueprintInput data)
        {
            TypeUpdate();
        }

        private void TypeUpdate()
        {
            FieldType ft = (FieldType)fields[0].GetValue(FieldType.Number);
            if (fields[1].fieldType != ft)
            {
                fields[1] = new Field(ft, "Number A", ConnectionType.DataInput, this, FieldContainer.Object);

                EBlueprint e = EBlueprint.GetBlueprintByNode(this);
                e.Connection_RemoveRelateConnectionInField(fields[1]);
            }
            if (fields[2].fieldType != ft)
            {
                fields[2] = new Field(ft, "Number B", ConnectionType.DataInput, this, FieldContainer.Object);

                EBlueprint e = EBlueprint.GetBlueprintByNode(this);
                e.Connection_RemoveRelateConnectionInField(fields[2]);
            }
        }
    }
}
