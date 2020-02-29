using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Transform/Set/SetGlobalPosition")]
    [Transform_Menu("TransformGlobal")]
    public class TransformSetGlobalPosition : NodeBase
    {
        public TransformSetGlobalPosition(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Global Position";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            GetFieldOrLastInputField<Transform>(1, data).position = GetFieldOrLastInputField<Vector3>(2, data);
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
