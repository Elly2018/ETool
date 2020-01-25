using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Get Axis")]
    public class AxisRaw : NodeBase
    {
        public AxisRaw(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get AxisRaw";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "AxisName", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 1)]
        public float GetResult(BlueprintInput data)
        {
            return Input.GetAxisRaw((string)GetFieldOrLastInputField(0, data));
        }
    }
}
