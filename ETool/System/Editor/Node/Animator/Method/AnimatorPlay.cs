using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Method/Play")]
    public class AnimatorPlay : NodeBase
    {
        public AnimatorPlay(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Animator Play";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.EventBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.String, "State Name", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Int, "Layer", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            Animator a = GetFieldOrLastInputField<Animator>(1, data);
            string v1 = GetFieldOrLastInputField<string>(2, data);
            int v2 = GetFieldOrLastInputField<int>(3, data);

            if (a != null)
                a.Play(v1, v2);

            ActiveNextEvent(0, data);
        }
    }
}
