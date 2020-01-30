using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Transform/Set Local Rotation")]
    public class SetLocalRotation : NodeBase
    {
        public SetLocalRotation(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Set Local Rotation";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            ((Transform)GetFieldOrLastInputField(1, data)).localRotation = (Quaternion)GetFieldOrLastInputField(2, data);
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Quaternion, "Value", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}