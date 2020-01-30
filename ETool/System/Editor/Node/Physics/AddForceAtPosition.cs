using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Physics/AddForceAtPosition")]
    public class AddForceAtPosition : NodeBase
    {
        public AddForceAtPosition(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "AddForceAtPosition";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(1, data);
            Vector3 v1 = (Vector3)GetFieldOrLastInputField(2, data);
            Vector3 v2 = (Vector3)GetFieldOrLastInputField(3, data);
            ForceMode v3 = (ForceMode)GetFieldOrLastInputField(4, data);
            if (r != null)
            {
                r.AddForceAtPosition(v1, v2, v3);
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Rigidbody, "Rigibody", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Force", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Position", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.ForceMode, "Mode", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}



