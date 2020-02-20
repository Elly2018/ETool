using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Transform/Get/GetLocalScale")]
    [Transform_Menu("TransformLocal")]
    public class TransformGetLocalScale : NodeBase
    {
        public TransformGetLocalScale(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Get Local Scale";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Transform, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Quaternion, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Vector3), 1)]
        public Vector3 GetString(BlueprintInput data)
        {
            Transform s0 = (Transform)GetFieldOrLastInputField(0, data);
            if (s0 == null) return Vector3.zero;
            return s0.localScale;
        }
    }
}