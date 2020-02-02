using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Casting/Collision/Breakdown")]
    public class Collision_Breakdown : NodeBase
    {
        public Collision_Breakdown(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Collision";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Collision, "Collision", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Relative Velocity", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Rigidbody, "Hit Rigibody", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Collider, "Hit Collider", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Hit Target", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Impulse", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 1)]
        public Vector3 GetVelocity(BlueprintInput data)
        {
            return ((Collision)GetFieldOrLastInputField(0, data)).relativeVelocity;
        }

        [NodePropertyGet(typeof(Rigidbody), 2)]
        public Rigidbody GetRigidbody(BlueprintInput data)
        {
            return ((Collision)GetFieldOrLastInputField(0, data)).rigidbody;
        }

        [NodePropertyGet(typeof(Collider), 3)]
        public Collider GetCollider(BlueprintInput data)
        {
            return ((Collision)GetFieldOrLastInputField(0, data)).collider;
        }

        [NodePropertyGet(typeof(Transform), 4)]
        public Transform GetTransform(BlueprintInput data)
        {
            return ((Collision)GetFieldOrLastInputField(0, data)).transform;
        }

        [NodePropertyGet(typeof(Vector3), 5)]
        public Vector3 GetImpulse(BlueprintInput data)
        {
            return ((Collision)GetFieldOrLastInputField(0, data)).impulse;
        }
    }
}
