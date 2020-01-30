using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Merge/Vector3")]
    public class Vector3_Merge : NodeBase
    {
        public Vector3_Merge(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Vector3";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "x", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "y", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "z", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 0)]
        public Vector3 GetResultVector3(BlueprintInput data)
        {
            return new Vector3(
                (float)GetFieldOrLastInputField(1, data),
                (float)GetFieldOrLastInputField(2, data),
                (float)GetFieldOrLastInputField(3, data));
        }
    }
}
