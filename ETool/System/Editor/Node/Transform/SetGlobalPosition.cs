using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Transform/Set Local Position")]
    public class SetGlobalPosition : NodeBase
    {
        public SetGlobalPosition(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Set Local Position";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            ((Transform)GetFieldOrLastInputField(1, data)).localPosition = (Vector3)GetFieldOrLastInputField(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Value", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}
