using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Transform/Get/GetInverseTransformVector")]
    [Transform_Menu("TransformSpace")]
    public class TransformGetInverseTransformVector : NodeBase
    {
        public TransformGetInverseTransformVector(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Inverse Transform Vector";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Transform, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Value", ConnectionType.DataOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 2)]
        public Vector3 GetResult(BlueprintInput data)
        {
            Transform t = GetFieldOrLastInputField<Transform>(0, data);
            Vector3 v = GetFieldOrLastInputField<Vector3>(1, data);

            if (t != null)
            {
                return t.InverseTransformVector(v);
            }

            return Vector3.zero;
        }
    }
}
