using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Array/Remove Element At")]
    public class RemoveElementAt : NodeBase
    {
        object[] dataBuffer;

        public RemoveElementAt(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Remove Element At";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            dataBuffer = GetFieldOrLastInputField<object[]>(3, data);
            int index = GetFieldOrLastInputField<int>(2, data);

            List<object> buffer = dataBuffer.ToList();
            buffer.RemoveAt(index);
            dataBuffer = buffer.ToArray();

            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Type, "Type", ConnectionType.None, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Remove Index", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Array", ConnectionType.DataBoth, true, this, FieldContainer.Array));
        }

        [NodePropertyGet(typeof(object), 3)]
        public object[] GetArray(BlueprintInput data)
        {
            return dataBuffer;
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
                fields[3] = new Field(ft, "Array", ConnectionType.DataBoth, true, this, FieldContainer.Array);
            }
        }


    }
}
