using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Array/Method/AddElement")]
    public class ArrayAddElement : NodeBase
    {
        private object elementBuffer;
        private object[] elementArrayBuffer;

        public ArrayAddElement(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Add Element";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            elementBuffer = GetFieldOrLastInputField<object>(2, data);
            elementArrayBuffer = GetFieldOrLastInputField<object[]>(3, data);

            object[] objlist = new object[elementArrayBuffer.Length + 1];
            for(int i = 0; i < elementArrayBuffer.Length; i++)
            {
                objlist[i] = elementArrayBuffer[i];
            }
            objlist[objlist.Length - 1] = elementBuffer;

            elementArrayBuffer = objlist;

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Adding Element", ConnectionType.DataBoth, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Array", ConnectionType.DataBoth, true, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(object), 2)]
        public object GetIndex(BlueprintInput data)
        {
            return elementBuffer;
        }

        [NodePropertyGet(typeof(object[]), 3)]
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
