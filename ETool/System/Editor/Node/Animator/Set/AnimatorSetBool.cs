using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Set/SetBool")]
    public class AnimatorSetBool : NodeBase
    {
        public AnimatorSetBool(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set Bool";
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Animator a = (Animator)GetFieldOrLastInputField(1, data);
            string n = (string)GetFieldOrLastInputField(2, data);
            bool v = (bool)GetFieldOrLastInputField(3, data);
            if (a != null)
            {
                a.SetBool(n, v);
            }
            ActiveNextEvent(0, data);
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "Name", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Boolean, "Value", ConnectionType.DataInput, this, FieldContainer.Object));
        }
    }
}

