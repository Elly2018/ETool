using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Breakdown/Collision2D")]
    public class Collision2D_Breakdown : NodeBase
    {
        public Collision2D_Breakdown(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "Collision2D";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Collision, "Collision", ConnectionType.DataInput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector2, "Relative Velocity", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Rigidbody2D, "Hit Rigibody", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Rigidbody2D, "Other Rigibody", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Collider, "Hit Collider", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Collider, "Other Collider", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Hit Target", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector2), 1)]
        public Vector2 GetVelocity(BlueprintInput data)
        {
            return ((Collision2D)GetFieldOrLastInputField(0, data)).relativeVelocity;
        }

        [NodePropertyGet(typeof(Rigidbody2D), 2)]
        public Rigidbody2D GetRigidbody(BlueprintInput data)
        {
            return ((Collision2D)GetFieldOrLastInputField(0, data)).rigidbody;
        }

        [NodePropertyGet(typeof(Rigidbody2D), 3)]
        public Rigidbody2D GetOtherRigidbody(BlueprintInput data)
        {
            return ((Collision2D)GetFieldOrLastInputField(0, data)).otherRigidbody;
        }

        [NodePropertyGet(typeof(Collider2D), 4)]
        public Collider2D GetCollider2D(BlueprintInput data)
        {
            return ((Collision2D)GetFieldOrLastInputField(0, data)).collider;
        }

        [NodePropertyGet(typeof(Collider2D), 5)]
        public Collider2D GetOtherCollider2D(BlueprintInput data)
        {
            return ((Collision2D)GetFieldOrLastInputField(0, data)).otherCollider;
        }

        [NodePropertyGet(typeof(Transform), 6)]
        public Transform GetTarget(BlueprintInput data)
        {
            return ((Collision2D)GetFieldOrLastInputField(0, data)).transform;
        }
    }
}
