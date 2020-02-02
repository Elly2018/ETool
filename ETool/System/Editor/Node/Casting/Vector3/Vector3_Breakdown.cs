using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Vector3/Breakdown")]
    public class Vector3_Breakdown : NodeBase
    {
        public Vector3_Breakdown(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Vector3";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "x", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "y", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "z", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 1)]
        public float GetResultVector3x(BlueprintInput data)
        {
            return ((Vector3)GetFieldOrLastInputField(0, data)).x;
        }

        [NodePropertyGet(typeof(float), 2)]
        public float GetResultVector3y(BlueprintInput data)
        {
            return ((Vector3)GetFieldOrLastInputField(0, data)).y;
        }

        [NodePropertyGet(typeof(float), 3)]
        public float GetResultVector3z(BlueprintInput data)
        {
            return ((Vector3)GetFieldOrLastInputField(0, data)).z;
        }
    }
}
