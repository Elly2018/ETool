using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Physics/AddForce")]
    public class AddForce : NodeBase
    {
        public AddForce(Vector2 position, float width, float height) : base(position, width, height)
        {
            title = "AddForce";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Rigidbody r = (Rigidbody)GetFieldOrLastInputField(1, data);
            Vector3 v = (Vector3)GetFieldOrLastInputField(2, data);
            ForceMode f = (ForceMode)GetFieldOrLastInputField(3, data);
            if(r != null)
            {
                r.AddForce(v, f);
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Rigidbody, "Rigibody", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Force", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.ForceMode, "Mode", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}


