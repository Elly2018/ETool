using System;
using UnityEngine;

namespace ETool.ANode
{
    public class AReturn : NodeBase
    {
        public AReturn(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Return";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            data.eventManager.SendReturn(GetFieldOrLastInputField<object>(1, data));
            data.eventManager.StartReturn(data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Value", ConnectionType.EventInput, this, FieldContainer.Object));
            SetReturnType();
        }

        public void SetReturnType()
        {
            if (fields[1].fieldType != returnType || fields[1].fieldContainer != returnContainer)
            {
                fields[1] = new Field(returnType, "Value", ConnectionType.DataInput, this, returnContainer);
            }
        }
    }
}
