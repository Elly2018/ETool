using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Method/Update")]
    public class AnimatorUpdate : NodeBase
    {
        public AnimatorUpdate(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Update";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Delta Time", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Animator a = GetFieldOrLastInputField<Animator>(1, data);
            float v1 = GetFieldOrLastInputField<float>(2, data);

            if (a != null)
                a.Update(v1);

            ActiveNextEvent(0, data);
        }
    }
}
