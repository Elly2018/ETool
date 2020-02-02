using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Set/SetIKRotation")]
    public class AnimatorSetIKRotation : NodeBase
    {
        public AnimatorSetIKRotation(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set IK Rotation";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AvatarIKGoal, "Goal", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Quaternion, "Rotation", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            AvatarIKGoal v1 = GetFieldOrLastInputField<AvatarIKGoal>(1, data);
            Quaternion v2 = GetFieldOrLastInputField<Quaternion>(2, data);
            Animator v3 = GetFieldOrLastInputField<Animator>(3, data);

            if (v3 != null)
                v3.SetIKRotation(v1, v2);

            ActiveNextEvent(0, data);
        }
    }
}


