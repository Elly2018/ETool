using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Vector2/Breakdown")]
    public class Vector2_Breakdown : NodeBase
    {
        public Vector2_Breakdown(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Vector2";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector2, "Result", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "x", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "y", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 1)]
        public float GetResultVector3x(BlueprintInput data)
        {
            return ((Vector2)GetFieldOrLastInputField(1, data)).x;
        }

        [NodePropertyGet(typeof(float), 2)]
        public float GetResultVector3y(BlueprintInput data)
        {
            return ((Vector2)GetFieldOrLastInputField(1, data)).y;
        }
    }
}
