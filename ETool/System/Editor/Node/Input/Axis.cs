using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Input/Get Axis")]
    public class Axis : NodeBase
    {
        public Axis(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Get Axis";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.String, "AxisName", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 1)]
        public float GetResult(BlueprintInput data)
        {
            return Input.GetAxis((string)GetFieldOrLastInputField(0, data));
        }
    }
}
