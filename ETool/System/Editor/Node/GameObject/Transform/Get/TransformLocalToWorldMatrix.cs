﻿using System;
using UnityEngine;

namespace ETool.ANode
{
    [NodePath("Add Node/GameObject/Transform/Method/LocalToWorldMatrix")]
    [Transform_Menu("TransformMatrix")]
    public class TransformLocalToWorldMatrix : NodeBase
    {
        public TransformLocalToWorldMatrix(Vector2 position, float width, float height) : base(position, width, height)
        {
            unlocalTitle = "Local To World Matrix";
        }

        public override void FieldInitialize()
        {
            fields.Add(new Field(FieldType.Matrix4x4, "Result", ConnectionType.DataOutput, true, this, FieldContainer.Object));
            fields.Add(new Field(FieldType.Transform, "Target", ConnectionType.DataInput, this, FieldContainer.Object));
        }

        [NodePropertyGet(typeof(Matrix4x4), 0)]
        public Matrix4x4 GetMatrix(BlueprintInput data)
        {
            return GetFieldOrLastInputField<Transform>(1, data).localToWorldMatrix;
        }
    }
}



