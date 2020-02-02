using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Float/=>Double")]
    public class FloatToDouble : NodeBase
    {
        public FloatToDouble(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Float => Double";
        }

        [NodePropertyGet(typeof(double), 1)]
        public double GetString(BlueprintInput data)
        {
            return (double)((float)GetFieldOrLastInputField(0, data));
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Float", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Double, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}

