using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Vector2/Merge")]
    public class Vector2_Merge : NodeBase
    {
        public Vector2_Merge(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Vector2";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "x", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "y", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector2), 0)]
        public Vector2 GetResultVector2(BlueprintInput data)
        {
            GameObject g;
            return new Vector2(
                (float)GetFieldOrLastInputField(1, data),
                (float)GetFieldOrLastInputField(2, data));
        }
    }
}