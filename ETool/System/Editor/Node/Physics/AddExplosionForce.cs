using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Physics/AddExplosionForce")]
    public class AddExplosionForce : NodeBase
    {
        public AddExplosionForce(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "AddExplosionForce";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(1, data);
            float v1 = (float)GetFieldOrLastInputField(2, data);
            Vector3 v2 = (Vector3)GetFieldOrLastInputField(3, data);
            float v3 = (float)GetFieldOrLastInputField(4, data);
            float v4 = (float)GetFieldOrLastInputField(5, data);
            ForceMode v5 = (ForceMode)GetFieldOrLastInputField(5, data);
            if (r != null)
            {
                r.AddExplosionForce(v1, v2, v3, v4, v5);
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Rigidbody, "Rigibody", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Force", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Position", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Radius", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Upward Modifier", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.ForceMode, "Mode", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}


