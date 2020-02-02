using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Set/SetIKRotationWeight")]
    public class AnimatorSetIKRotationWeight : NodeBase
    {
        public AnimatorSetIKRotationWeight(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set IK Rotation Weight";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AvatarIKGoal, "Goal", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Value", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            AvatarIKGoal v1 = GetFieldOrLastInputField<AvatarIKGoal>(1, data);
            float v2 = GetFieldOrLastInputField<float>(2, data);
            Animator v3 = GetFieldOrLastInputField<Animator>(3, data);

            if (v3 != null)
                v3.SetIKRotationWeight(v1, v2);

            ActiveNextEvent(0, data);
        }
    }
}


