using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Math/Vector/Vector3SmoothDamp")]
    [Math_Menu("Vector3")]
    public class SmoothDampVector3 : NodeBase
    {
        private Vector3 velocity = Vector3.zero;

        public SmoothDampVector3(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "SmoothDamp";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Current", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Target", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Velocity", ConnectionType.DataOutput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "SmoothTime", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 0)]
        public Vector3 GetResultVector3(BlueprintInput data)
        {
            Vector3 current = GetFieldOrLastInputField<Vector3>(1, data);
            Vector3 target = GetFieldOrLastInputField<Vector3>(2, data);
            float smooth = GetFieldOrLastInputField<float>(4, data);

            Vector3 result = Vector3.SmoothDamp(current, target, ref velocity, smooth);
            return result;
        }

        [NodePropertyGet(typeof(Vector3), 1)]
        public Vector3 GetCurrent(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Vector3>(1, data);
        }

        [NodePropertyGet(typeof(Vector3), 2)]
        public Vector3 GetTarget(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Vector3>(2, data);
        }

        [NodePropertyGet(typeof(Vector3), 3)]
        public Vector3 GetVelocity(BlueprintInput data)
        {
            return velocity;
        }
    }
}

