using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Breakdown/Rigidbody")]
    public class Rigidbody_Breakdown : NodeBase
    {
        public Rigidbody_Breakdown(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Rigidbody";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Rigidbody, "Rigidbody", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Mass", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Drag", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Angular Drag", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Use Gravity", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Is Kinematic", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Interpolation, "Interpolation", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.DetectionMode, "DetectionMode", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 1)]
        public float GetMass(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(0, data);
            return r.mass;
        }

        [NodePropertyGet(typeof(Single), 2)]
        public float GetDrag(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(0, data);
            return r.drag;
        }

        [NodePropertyGet(typeof(Single), 3)]
        public float GetADrag(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(0, data);
            return r.angularDrag;
        }

        [NodePropertyGet(typeof(Boolean), 4)]
        public bool GetG(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(0, data);
            return r.useGravity;
        }

        [NodePropertyGet(typeof(Boolean), 5)]
        public bool GetIsK(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(0, data);
            return r.isKinematic;
        }

        [NodePropertyGet(typeof(RigidbodyInterpolation), 6)]
        public RigidbodyInterpolation GetRigidbodyInterpolation(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(0, data);
            return r.interpolation;
        }

        [NodePropertyGet(typeof(CollisionDetectionMode), 7)]
        public CollisionDetectionMode GetDetectionMode(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(0, data);
            return r.collisionDetectionMode;
        }
    }
}
