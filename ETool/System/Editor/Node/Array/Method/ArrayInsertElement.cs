using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Array/Method/InsertElement")]
    public class ArrayInsertElement : NodeBase
    {
        private object elementBuffer;
        private object[] elementArrayBuffer;

        public ArrayInsertElement(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Insert Element";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            int index = GetFieldOrLastInputField<int>(2, data);
            elementBuffer = GetFieldOrLastInputField<object>(3, data);
            elementArrayBuffer = GetFieldOrLastInputField<object[]>(4, data);

            List<object> buffer = new List<object>();
            buffer.Insert(index, elementBuffer);
            elementArrayBuffer = buffer.ToArray();

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Index", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Insert", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Array", ConnectionType.DataBoth, true, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(object), 3)]
        public object GetIndex(BlueprintInput data)
        {
            return elementBuffer;
        }

        [NodePropertyGet(typeof(object), 4)]
        public object[] GetArray(BlueprintInput data)
        {
            return elementArrayBuffer;
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
            FieldType ft = (FieldType)fields[1].GetValue(FieldType.Type);
            if (fields[3].fieldType != ft)
            {
                fields[3] = new Field(ft, "Select", ConnectionType.DataBoth, true, this, FieldContainer.Object);
            }
            if (fields[4].fieldType != ft)
            {
                fields[4] = new Field(ft, "Array", ConnectionType.DataBoth, true, this, FieldContainer.Array);
            }
        }
    }
}

