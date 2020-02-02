using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Get/GetIKRotation")]
    public class AnimatorGetIKRotation : NodeBase
    {
        public AnimatorGetIKRotation(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "GetIKRotation";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.AvatarIKGoal, "Goal", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Quaternion, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Quaternion), 2)]
        public Quaternion GetWeight(BlueprintInput data)
        {
            AvatarIKGoal v1 = GetFieldOrLastInputField<AvatarIKGoal>(0, data);
            Animator v2 = GetFieldOrLastInputField<Animator>(1, data);
            if (v2 != null)
                return v2.GetIKRotation(v1);
            else
                return Quaternion.identity;
        }
    }
}

