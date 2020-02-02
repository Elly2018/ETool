using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Set/SetIKPosition")]
    public class AnimatorSetIKPosition : NodeBase
    {
        public AnimatorSetIKPosition(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Set IK Position";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Event, "Event", ConnectionType.DataBoth, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.AvatarIKGoal, "Goal", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Vector3, "Position", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        public override void ProcessCalling(BlueprintInput data)
        {
            AvatarIKGoal v1 = GetFieldOrLastInputField<AvatarIKGoal>(1, data);
            Vector3 v2 = GetFieldOrLastInputField<Vector3>(2, data);
            Animator v3 = GetFieldOrLastInputField<Animator>(3, data);

            if (v3 != null)
                v3.SetIKPosition(v1, v2);

            ActiveNextEvent(0, data);
        }
    }
}
