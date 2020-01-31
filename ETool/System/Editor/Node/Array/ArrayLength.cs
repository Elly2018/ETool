using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Array/Array Length")]
    public class ArrayLength : NodeBase
    {
        public ArrayLength(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Array Length";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Array", ConnectionType.DataInput, true, this, FieldContainer.Array));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        public override void FieldUpdate()
        {
            FieldUpdateType();
        }

        public override void FinalFieldInitialize(BlueprintInput data)
        {
            FieldUpdateType();
        }

        private void FieldUpdateType()
        {
            FieldType ft = (FieldType)fields[0].GetValue(FieldType.Type);
            if (fields[1].fieldType != ft)
            {
                fields[1] = new Field(ft, "Array", ConnectionType.DataInput, true, this, FieldContainer.Array);
            }
        }

        [NodePropertyGet(typeof(Int32), 2)]
        public int GetLength(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Array>(1, data).Length;
        }
    }
}


