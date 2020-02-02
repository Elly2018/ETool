using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Int/=>Double")]
    public class IntToDouble : NodeBase
    {
        public IntToDouble(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Int => Double";
        }

        [NodePropertyGet(typeof(double), 1)]
        public double GetString(BlueprintInput data)
        {
            return (double)((int)GetFieldOrLastInputField(0, data));
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Int", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Double, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}

