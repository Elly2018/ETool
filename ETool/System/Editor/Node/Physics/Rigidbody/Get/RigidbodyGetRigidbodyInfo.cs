using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Physics/Rigidbody/Get/GetRigidbodyInfo")]
    public class RigidbodyGetRigidbodyInfo : NodeBase
    {
        public RigidbodyGetRigidbodyInfo(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Rigidbody Info";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Float, "Speed", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Velocity", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Abgular Velocity", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Rigidbody, "Rigibody", ConnectionType.DataInput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Single), 0)]
        public Single GetSpeed(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(3, data);
            return r.velocity.magnitude;
        }

        [NodePropertyGet(typeof(Vector3), 0)]
        public Vector3 GetVelocity(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(3, data);
            return r.velocity;
        }

        [NodePropertyGet(typeof(Vector3), 0)]
        public Vector3 GetAngularVelocity(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(3, data);
            return r.angularVelocity;
        }
    }
}



