using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Vector/DotVector3")]
    public class DotVector3 : NodeBase
    {
        public DotVector3(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Dot";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "First", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Second", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 0)]
        public float GetResultVector2(BlueprintInput data)
        {
            return Vector3.Dot((Vector3)GetFieldOrLastInputField(1, data), (Vector3)GetFieldOrLastInputField(2, data));
        }
    }
}

