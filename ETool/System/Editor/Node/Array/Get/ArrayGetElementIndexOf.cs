using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Array/Get/GetElementIndexOf")]
    public class ArrayGetElementIndexOf : NodeBase
    {
        public ArrayGetElementIndexOf(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Element Index Of";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Taregt", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Array", ConnectionType.DataInput, true, this, FieldContainer.Array));
            fields.Add(new Field(FieldType.Int, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(int), 3)]
        public int GetIndex(BlueprintInput data)
        {
            object index = GetFieldOrLastInputField<object>(1, data);
            object[] array = GetFieldOrLastInputField<object[]>(2, data);
            List<object> arrayList = array.ToList();

            if (arrayList != null && index != null)
                if(arrayList.Contains(index))
                    return arrayList.IndexOf(index);

            return -1;
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
                fields[1] = new Field(ft, "Target", ConnectionType.DataInput, true, this, FieldContainer.Object);
            }
            if (fields[2].fieldType != ft)
            {
                fields[2] = new Field(ft, "Array", ConnectionType.DataInput, true, this, FieldContainer.Array);
            }
        }
    }
}

