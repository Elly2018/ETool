using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Vector/Two Vector3 Distance")]
    public class TwoVector3Distance : NodeBase
    {
        public TwoVector3Distance(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Two Vector3 Distance";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "First", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Second", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 0)]
        public float GetResultVector2(BlueprintInput data)
        {
            return Vector3.Distance((Vector3)GetFieldOrLastInputField(1, data), (Vector3)GetFieldOrLastInputField(1, data));
        }
    }
}

