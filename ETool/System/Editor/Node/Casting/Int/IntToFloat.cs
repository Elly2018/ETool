using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Int/=>Float")]
    public class IntToFloat : NodeBase
    {
        public IntToFloat(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Int => Float";
        }

        [NodePropertyGet(typeof(float), 1)]
        public float GetString(BlueprintInput data)
        {
            return (float)((int)GetFieldOrLastInputField(0, data));
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Int, "Int", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}


