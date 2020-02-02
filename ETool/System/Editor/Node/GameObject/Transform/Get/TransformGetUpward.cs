using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Transform/Get/GetUpward")]
    public class TransformGetUpward : NodeBase
    {
        public TransformGetUpward(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Upward";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Transform, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 1)]
        public Vector3 GetResult(BlueprintInput data)
        {
            Transform t = GetFieldOrLastInputField<Transform>(0, data);

            if (t != null)
                return t.up;

            return Vector3.zero;
        }
    }
}

