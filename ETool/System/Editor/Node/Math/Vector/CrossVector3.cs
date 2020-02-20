using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Vector/Vector3Cross")]
    [Math_Menu("Vector3")]
    public class Vector3Cross : NodeBase
    {
        public Vector3Cross(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Cross";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "First", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Second", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 0)]
        public Vector3 GetResultVector2(BlueprintInput data)
        {
            return Vector3.Cross((Vector3)GetFieldOrLastInputField(1, data), (Vector3)GetFieldOrLastInputField(2, data));
        }
    }
}

