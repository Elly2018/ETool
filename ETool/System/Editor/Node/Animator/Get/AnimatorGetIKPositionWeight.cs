﻿using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/Animator/Get/GetIKPositionWeight")]
    public class AnimatorGetIKPositionWeight : NodeBase
    {
        public AnimatorGetIKPositionWeight(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "GetIKPositionWeight";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.AvatarIKGoal, "Goal", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Animator, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Float, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(float), 2)]
        public float GetWeight(BlueprintInput data)
        {
            AvatarIKGoal v1 = GetFieldOrLastInputField<AvatarIKGoal>(0, data);
            Animator v2 = GetFieldOrLastInputField<Animator>(1, data);
            if (v2 != null)
                return v2.GetIKPositionWeight(v1);
            else
                return -1.0f;
        }
    }
}
