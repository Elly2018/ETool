using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Vector/Vector2Dot")]
    [Math_Menu("Vector2")]
    public class Vector2Dot : NodeBase
    {
        public Vector2Dot(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Dot";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector2, "First", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector2, "Second", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 0)]
        public float GetResultVector2(BlueprintInput data)
        {
            return Vector2.Dot((Vector2)GetFieldOrLastInputField(1, data), (Vector2)GetFieldOrLastInputField(2, data));
        }
    }
}

