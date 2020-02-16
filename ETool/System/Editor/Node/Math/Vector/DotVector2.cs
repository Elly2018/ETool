using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Vector/DotVector2")]
    public class DotVector2 : NodeBase
    {
        public DotVector2(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Dot";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, this, FieldContainer.Object));
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

