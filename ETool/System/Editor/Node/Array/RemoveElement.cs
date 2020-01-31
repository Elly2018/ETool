using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Array/Remove Element")]
    public class RemoveElement : NodeBase
    {
        private object elementBuffer;
        private object[] elementArrayBuffer;

        public RemoveElement(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Remove Element";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            elementBuffer = GetFieldOrLastInputField<object>(2, data);
            elementArrayBuffer = GetFieldOrLastInputField<object[]>(3, data);

            List<object> buffer = elementArrayBuffer.ToList();
            buffer.Remove(elementBuffer);
            elementArrayBuffer = buffer.ToArray();

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Remove Element", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Array", ConnectionType.DataBoth, true, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(object), 2)]
        public object GetIndex(BlueprintInput data)
        {
            return elementBuffer;
        }

        [NodePropertyGet(typeof(object), 3)]
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
            if (fields[2].fieldType != ft)
            {
                fields[2] = new Field(ft, "Select", ConnectionType.DataBoth, true, this, FieldContainer.Object);
            }
            if (fields[3].fieldType != ft)
            {
                fields[3] = new Field(ft, "Array", ConnectionType.DataBoth, true, this, FieldContainer.Array);
            }
        }
    }
}
