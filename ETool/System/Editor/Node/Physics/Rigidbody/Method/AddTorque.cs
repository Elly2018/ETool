﻿using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Physics/AddTorque")]
    public class AddTorque : NodeBase
    {
        public AddTorque(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "AddTorque";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(1, data);
            Vector3 v = (Vector3)GetFieldOrLastInputField(2, data);
            ForceMode f = (ForceMode)GetFieldOrLastInputField(3, data);
            if (r != null)
            {
                r.AddTorque(v, f);
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Rigidbody, "Rigibody", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Torque", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.ForceMode, "Mode", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}



