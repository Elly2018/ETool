using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Matrix/=>String")]
    public class MatrixToString : NodeBase
    {
        public MatrixToString(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Matrix => String";
        }

        [NodePropertyGet(typeof(string), 1)]
        public string GetString(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Matrix4x4>(0, data).ToString();
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Matrix4x4, "Int", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }
    }
}

