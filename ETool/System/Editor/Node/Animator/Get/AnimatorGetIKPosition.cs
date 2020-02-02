using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Get/GetIKPosition")]
    public class AnimatorGetIKPosition : NodeBase
    {
        public AnimatorGetIKPosition(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "GetIKPosition";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.AvatarIKGoal, "Goal", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 2)]
        public Vector3 GetWeight(BlueprintInput data)
        {
            AvatarIKGoal v1 = GetFieldOrLastInputField<AvatarIKGoal>(0, data);
            Animator v2 = GetFieldOrLastInputField<Animator>(1, data);
            if (v2 != null)
                return v2.GetIKPosition(v1);
            else
                return Vector3.zero;
        }
    }
}

