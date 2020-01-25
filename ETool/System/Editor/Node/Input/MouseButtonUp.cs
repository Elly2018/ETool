using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Get Mouse Button Up")]
    public class MouseButtonUp : NodeBase
    {
        public MouseButtonUp(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get Mouse Button Up";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Button Index", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Boolean), 1)]
        public bool GetResult(BlueprintInput data)
        {
            return Input.GetMouseButtonUp((int)GetFieldOrLastInputField(0, data));
        }
    }
}


