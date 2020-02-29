using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Transform/Set/SetGlobalRotation")]
    [Transform_Menu("TransformGlobal")]
    public class TransformSetGlobalRotation : NodeBase
    {
        public TransformSetGlobalRotation(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Global Rotation";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<Transform>(1, data).rotation = GetFieldOrLastInputField<Quaternion>(2, data);
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
