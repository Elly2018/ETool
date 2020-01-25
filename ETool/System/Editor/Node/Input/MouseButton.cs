using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Get Mouse Button")]
    public class MouseButton : NodeBase
    {
        public MouseButton(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get Mouse Button";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Button Index", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 1)]
        public bool GetResult(BlueprintInput data)
        {
            return Input.GetMouseButton((int)GetFieldOrLastInputField(0, data));
        }
    }
}

