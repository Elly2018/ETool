using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Array/Get/GetElementAt")]
    public class ArrayGetElementAt : NodeBase
    {
        public ArrayGetElementAt(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Element At";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Index", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Array", ConnectionType.DataInput, true, this, FieldContainer.Array));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(object), 3)]
        public object GetIndex(BlueprintInput data)
        {
            int index = GetFieldOrLastInputField<int>(1, data);
            object[] array = GetFieldOrLastInputField<object[]>(2, data);

            if (array != null)
                return array[index];
            else
                return null;
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
            if (fields[2].fieldType != ft)
            {
                fields[2] = new Field(ft, "Array", ConnectionType.DataInput, true, this, FieldContainer.Array);
            }
            if (fields[3].fieldType != ft)
            {
                fields[3] = new Field(ft, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object);
            }
        }
    }
}

